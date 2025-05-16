using System;
using System.Linq;
using System.Reflection;

using UnityEngine.UIElements;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.UnityUICore.Mvvm {



public static class MultiColumnListViewExtensions
{
	public static void BindViewModel<TRowVM>(this MultiColumnListView control, ITableVM<TRowVM> viewModel)
	{
		control.itemsSource = viewModel.ItemsSource;

		var itemType = typeof(TRowVM);
		var properties = itemType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

		foreach (var property in properties) {
			var name = property.Name;

			var column = GetColumn(control, name);
			if (column == null) {
				continue;
			}

			column.makeCell = () => MakeCell(property.PropertyType);

			column.bindCell = (element, index) => BindCell(element, index, name, viewModel);
		}
	}



	private static Column? GetColumn(MultiColumnListView control, string propertyName)
	{
		return control.columns.FirstOrDefault(it => NamesMatch(propertyName, it.name));
	}


	private static bool NamesMatch(string propertyName, string columnName)
	{
		return propertyName.Equals(columnName, StringComparison.OrdinalIgnoreCase);
	}


	private static VisualElement MakeCell(Type propertyType)
	{
		if (propertyType.IsPrimitive)
			return new Label();
		if (propertyType == typeof(ISelectableVM<string>))
			return new DropdownField();

		return new Label();
	}


	private static void BindCell(VisualElement element, int index, string propertyName, ITableVM viewModel)
	{
		var cellViewModel = viewModel.GetCellViewModel(index, propertyName);
		BindCellViewModel(element, cellViewModel);
	}


	private static void BindCellViewModel(VisualElement element, object cellViewModel)
	{
		switch (cellViewModel) {
			case IValueVM<string> vm: (element as TextElement).BindViewModel(vm); break;
			case IValueVM<uint> vm: (element as TextElement).BindViewModel(vm); break;
			case IValueVM<int> vm: (element as TextElement).BindViewModel(vm); break;

			case ISelectableVM<string> vm: (element as DropdownField).BindViewModel(vm); break;
		}
	}
}



}
