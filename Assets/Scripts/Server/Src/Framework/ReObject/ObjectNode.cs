using System.Collections.Generic;



namespace Civ.Server.Framework.ReObject {



public struct OwnIdInObject : IOwnId
{
	public readonly string Name;


	public OwnIdInObject(string name)
	{
		Name = name;
	}
}



public class ObjectNode : Node
{
	protected readonly Dictionary<string, Node> _Nodes = new();
	protected readonly Dictionary<string, Property> _Properties = new();



	public ObjectNode()
	{}

	public ObjectNode(Node? parent, IOwnId ownId)
		: base(parent, ownId)
	{}

	public ObjectNode(Node parent, string ownId)
		: this(parent, new OwnIdInObject(ownId))
	{}

	public ObjectNode(Node parent, int ownId)
		: this(parent, new OwnIdInList(ownId))
	{}

	public ObjectNode(RootNodeId ownId)
		: this(null, ownId)
	{}


	public ObjectNode _Object(string path)
	{
		return (ObjectNode) _Nodes[path];
	}

	public ListNode _List(string path)
	{
		return (ListNode) _Nodes[path];
	}

	public Property<T> _Property<T>(string path)
	{
		return (Property<T>) _Properties[path];
	}
}



}
