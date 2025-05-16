using System.Collections.Generic;



namespace Civ.Server.Framework.ReObject {



public struct OwnIdInList : IOwnId
{
	public readonly int Index;


	public OwnIdInList(int index)
	{
		Index = index;
	}
}



public abstract class ListNode : Node
{
	public ListNode()
	{}

	public ListNode(Node? parent, IOwnId ownId)
		: base(parent, ownId)
	{}
}



public class ListNode<T> : ListNode
	where T : Element, new()
{
	private readonly List<T> _items = new();



	public ListNode()
	{}

	public ListNode(Node? parent, IOwnId ownId)
		: base(parent, ownId)
	{}

	public ListNode(Node parent, string ownId)
		: this(parent, new OwnIdInObject(ownId))
	{}

	public ListNode(Node parent, int ownId)
		: this(parent, new OwnIdInList(ownId))
	{}

	public ListNode(RootNodeId ownId)
		: this(null, ownId)
	{}



	public T AddDefault()
	{
		var @event = new PreAddEvent(_OwnId);
		DispatchEvent(@event);

		T item;

		var itemToAdd = @event.ItemToAdd;

		if (itemToAdd != null)
			item = itemToAdd;
		else
			item = new T();

		item.AttachToParent(this, _items.Count);
		_items.Add(item);

		return item;
	}
}



}
