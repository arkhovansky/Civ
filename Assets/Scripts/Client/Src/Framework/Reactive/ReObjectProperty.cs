using Cysharp.Threading.Tasks;

using Civ.Common.ClientServerProtocol;



namespace Civ.Client.Framework.Reactive {



public class ReObjectProperty
{
	public ReObject ReObject { get; }
	public PropertyPath PropertyPath { get; }



	public ReObjectProperty(ReObject reObject, PropertyPath propertyPath)
	{
		ReObject = reObject;
		PropertyPath = propertyPath;
	}


	public PropertyValue<T> GetValue<T>()
	{
		return ReObject.GetPropertyValue<T>(PropertyPath);
	}


	public async UniTask SaveValue<T>(T value)
	{
		await ReObject.SavePropertyValue(PropertyPath, value);
	}


	public void SubscribePropertyObserver(IPropertyObserver observer)
	{
		ReObject.SubscribePropertyObserver(PropertyPath, observer);
	}
}



}
