namespace Civ.Server.Framework.ReObject {



public abstract class Node : Element
{
	public Node()
	{}

	public Node(Node? parent, IOwnId ownId)
		: base(parent, ownId)
	{}
}



}
