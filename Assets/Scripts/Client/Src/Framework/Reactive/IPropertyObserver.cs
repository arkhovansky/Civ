namespace Civ.Client.Framework.Reactive {



public interface IPropertyObserver {}



public interface IPropertyObserver<T> : IPropertyObserver
{
	void OnPropertyValueChanged(PropertyValue<T> value);
}



}
