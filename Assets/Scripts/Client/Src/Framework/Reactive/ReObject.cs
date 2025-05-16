using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using Civ.Common.ClientServerProtocol;

using Civ.Client.Server;



namespace Civ.Client.Framework.Reactive {



public class ReObject
	: ObjectBase
	, IObjectView
{
	public RootObjectId ObjectId { get; }



	private readonly IServerProtocol _serverProtocol;

	private readonly Dictionary<PropertyPath, IPropertyObserver> _observers = new();



	public ReObject(RootObjectId objectId,
	                Dictionary<string, INode>? nodes, Dictionary<string, IProperty>? properties,
	                IServerProtocol serverProtocol)
		: base(nodes, properties)
	{
		ObjectId = objectId;
		_serverProtocol = serverProtocol;
	}


	public ReadOnlyReList GetReadOnlyList(PropertyPath path)
	{
		var node = (ListNode) GetNode(path.Segments);
		return new ReadOnlyReList(this, path, node);
	}

	public ReList GetList(PropertyPath path)
	{
		var node = (ListNode) GetNode(path.Segments);
		return new ReList(this, path, node);
	}


	public async UniTask SavePropertyValue<T>(PropertyPath path, T value)
	{
		var request = new Request(ObjectId, "Update", new ObjectUpdateData(path, value));
		await _serverProtocol.ExecuteRequest(request);

		SetPropertyValue(path, value);
	}


	public void SubscribePropertyObserver(PropertyPath path, IPropertyObserver observer)
	{
		_observers[path] = observer;
	}



	// IObjectView implementation

	public void OnPropertyValueUpdated<T>(PropertyPath path, T value)
	{
		SetPropertyValue(path, value);

		if (_observers.TryGetValue(path, out var observer))
			((IPropertyObserver<T>)observer).OnPropertyValueChanged(new PropertyValue<T>(value));
	}



	private INode GetNode(object[] path)
	{
		INode node = this;

		for (var i = 0; i < path.Length; ++i) {
			var segment = path[i];

			switch (segment) {
				case string name:
					node = ((ObjectBase)node).GetNode(name);

					break;

				case int:
				case uint:
					node = ((ListNode)node).GetItem((int)segment);

					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		return node;
	}
}



public interface INode
{
	PropertyValue<T> GetPropertyValue<T>(object[] path);

	void SetPropertyValue<T>(object[] path, T value);
}



public class ObjectBase : INode
{
	protected readonly Dictionary<string, INode>? Nodes;
	protected readonly Dictionary<string, IProperty>? Properties;



	public ObjectBase(Dictionary<string, INode>? nodes, Dictionary<string, IProperty>? properties)
	{
		Nodes = nodes;
		Properties = properties;
	}

	public ObjectBase(Dictionary<string, INode> nodes)
		: this(nodes, null)
	{}

	public ObjectBase(Dictionary<string, IProperty> properties)
		: this(null, properties)
	{}



	public PropertyValue<T> GetPropertyValue<T>(PropertyPath path)
	{
		return GetPropertyValue<T>(path.Segments);
	}

	public PropertyValue<T> GetPropertyValue<T>(object[] path)
	{
		if (path.Length == 0)
			throw new ArgumentOutOfRangeException();

		var name = (string) path[0];

		if (path.Length == 1) {
			return ((Property<T>) Properties[name]).Value;
		}

		var node = Nodes[name];
		return node.GetPropertyValue<T>(path[1..]);
	}



	protected void SetPropertyValue<T>(PropertyPath path, T value)
	{
		SetPropertyValue(path.Segments, value);
	}

	public void SetPropertyValue<T>(object[] path, T value)
	{
		if (path.Length == 0)
			throw new ArgumentOutOfRangeException();

		var segment = (string) path[0];

		if (path.Length == 1) {
			var property = (Property<T>)Properties[segment];
			var propertyValue = property.Value;
			propertyValue.Set(value);
			property.Value = propertyValue;

			return;
		}

		var node = Nodes[segment];
		node.SetPropertyValue(path[1..], value);
	}


	internal INode GetNode(string nodeName)
	{
		return Nodes[nodeName];
	}
}



public class ListNode : INode
{
	private readonly List<object> _items = new();



	internal uint Count => (uint) _items.Count;



	public PropertyValue<T> GetPropertyValue<T>(object[] path)
	{
		if (path.Length == 0)
			throw new ArgumentOutOfRangeException();

		var index = (int) path[0];

		if (path.Length == 1)
			return ((Property<T>) _items[index]).Value;

		var node = (INode) _items[index];
		return node.GetPropertyValue<T>(path[1..]);
	}


	public void SetPropertyValue<T>(object[] path, T value)
	{
		if (path.Length == 0)
			throw new ArgumentOutOfRangeException();

		var index = (int) path[0];

		if (path.Length == 1) {
			var property = (Property<T>) _items[index];
			var propertyValue = property.Value;
			propertyValue.Set(value);
			property.Value = propertyValue;

			return;
		}

		var node = (INode) _items[index];
		node.SetPropertyValue(path[1..], value);
	}



	internal INode GetItem(int index)
	{
		return (INode) _items[index];
	}


	internal void Add<T>(T value)
	{
		var property = new Property<T>();
		property.Value = new PropertyValue<T>(value);
		_items.Add(property);
	}
}



}
