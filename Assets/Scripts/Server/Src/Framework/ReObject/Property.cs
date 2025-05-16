namespace Civ.Server.Framework.ReObject {



public abstract class Property : Element
{
	public Property()
	{}

	public Property(Node? parent, IOwnId ownId)
		: base(parent, ownId)
	{}
}



public class Property<T> : Property
{
	private T _value;



	public Property()
	{}

	public Property(Node parent, IOwnId ownId)
		: base(parent, ownId)
	{}

	public Property(Node parent, string ownId)
		: this(parent, new OwnIdInObject(ownId))
	{}

	public Property(Node parent, int ownId)
		: this(parent, new OwnIdInList(ownId))
	{}



	public T Get() => _value;


	public void Set(T value)
	{
		_value = value;
	}
}



}
