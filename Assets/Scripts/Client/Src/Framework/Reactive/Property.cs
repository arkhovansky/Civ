namespace Civ.Client.Framework.Reactive {



public struct PropertyValue<T>
{
	public T Value { get; private set; }


	public PropertyValue(T value)
	{
		Value = value;
	}


	public void Set(T value)
	{
		Value = value;
	}
}



public interface IProperty {}



public class Property<T> : IProperty
{
	public PropertyValue<T> Value { get; set; }
}



}
