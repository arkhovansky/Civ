using System.Collections;



namespace Civ.Client.Framework.UICore.Mvvm {



public interface ITableVM
{
	IList ItemsSource { get; }

	object GetCellViewModel(int rowIndex, string propertyName);
}



public interface ITableVM<TRowVM> : ITableVM {}



}
