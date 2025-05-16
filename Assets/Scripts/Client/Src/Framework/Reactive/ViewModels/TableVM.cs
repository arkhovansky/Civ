using System.Collections;
using System.Collections.Generic;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.Reactive.ViewModels {



public class TableVM<TRowVM> : ITableVM
{
	private readonly List<TRowVM> _items;


	public IList ItemsSource => _items;



	public TableVM(List<TRowVM> itemsSource)
	{
		_items = itemsSource;
	}


	public object GetCellViewModel(int rowIndex, string propertyName)
	{
		var rowVM = _items[rowIndex];
		var property = rowVM.GetType().GetProperty(propertyName);

		return property.GetValue(rowVM);
	}
}



}
