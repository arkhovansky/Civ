namespace Civ.Client.Framework.UICore.Mvvm {



public interface IValueAdapter<TProperty, TVM>
{
	TVM VMFromProperty(TProperty propertyValue);
}



}
