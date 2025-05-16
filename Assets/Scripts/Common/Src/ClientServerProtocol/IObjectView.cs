namespace Civ.Common.ClientServerProtocol {



public interface IObjectView
{
	void OnPropertyValueUpdated<T>(PropertyPath path, T value);
}



}
