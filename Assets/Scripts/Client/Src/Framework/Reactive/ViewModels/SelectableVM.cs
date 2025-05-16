using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.Reactive.ViewModels {



public class SelectableVM<TVM, TProperty>
	: ISelectableVM<TVM>, IPropertyObserver<TProperty>
{
	public IReadOnlyList<TVM> Choices { get; }

	public VMField<uint?> Index { get; }

	public VMField<bool> IsChanged { get; }

	public VMField<bool> IsSynchronizing { get; }
	// public VMField<bool> IsLoading { get; }
	public VMField<bool> IsSaving { get; }



	private readonly ReObjectProperty _reObjectProperty;
	private readonly IReadOnlyList<TProperty> _choices;

	private uint? _savingIndex;



	public SelectableVM(ReObjectProperty reObjectProperty,
	                    IReadOnlyList<TProperty> choices, IValueAdapter<TProperty, TVM> valueAdapter)
	{
		_reObjectProperty = reObjectProperty;
		_choices = choices;

		Choices = _choices.Select(valueAdapter.VMFromProperty).ToList();

		var propertyValue = _reObjectProperty.GetValue<TProperty>().Value;
		var index = _choices.IndexOf(propertyValue);
		Index = new VMField<uint?>((uint)index);

		IsChanged = new VMField<bool>(false);
		IsSynchronizing = new VMField<bool>(false);
		IsSaving = new VMField<bool>(false);

		_reObjectProperty.SubscribePropertyObserver(this);
	}



	public void OnChange(string textValue, int index)
	{
		Debug.Log($"OnChange: {textValue}, {index}");

		// if (index == Index.Value) {
		// 	IsChanged.Set(false);
		// 	return;
		// }
		//
		// Index.SetWithoutNotification(index);
		//
		// IsChanged.Set(true);
		//
		// Save(_choices[index], index);
	}

	public void OnValueChanged(string textValue, int index)
	{
		Debug.Log($"OnValueChanged: {textValue}, {index}");

		// if (index == Index.Value) {
		// 	IsChanged.Set(false);
		// 	return;
		// }
		//
		// Index.SetWithoutNotification(index);
		//
		// IsChanged.Set(true);
		//
		// Save(_choices[index], index);
	}



	private async void Save(TProperty value, uint index)
	{
		IsSynchronizing.Set(true);
		IsSaving.Set(true);

		_savingIndex = index;

		await _reObjectProperty.SaveValue(value);

		Index.Set(_savingIndex);
		_savingIndex = null;
		IsChanged.Set(false);
		IsSynchronizing.Set(false);
		IsSaving.Set(false);
	}


	// IPropertyObserver

	public void OnPropertyValueChanged(PropertyValue<TProperty> value)
	{
		Index.Set((uint) _choices.IndexOf(value.Value));
		_savingIndex = null;
		IsChanged.Set(false);
		IsSynchronizing.Set(false);
		IsSaving.Set(false);
	}
}



internal static class EnumerableExtensions
{
	public static int IndexOf<T>(this IEnumerable<T> self, T elementToFind)
	{
		int i = 0;
		foreach (var element in self) {
			if (Equals(element, elementToFind))
				return i;
			++i;
		}
		return -1;
	}
}



}
