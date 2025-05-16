// using System;
//
// using Civ.Client.UICore.Mvvm;
//
//
//
// namespace Civ.Client.Framework.Reactive.ViewModels {
//
//
//
// public class IntegerFieldVM : IEditableFieldVM<int>, IPropertyObserver
// {
// 	public VMField<int?> Value { get; }
//
// 	public VMField<bool> IsChanged { get; }
//
// 	// public VMField<bool> IsValid { get; }
//
// 	public VMField<bool> IsSynchronizing { get; }
// 	// public VMField<bool> IsLoading { get; }
// 	public VMField<bool> IsSaving { get; }
//
//
//
// 	private readonly ReObject _reObject;
// 	private readonly PropertyPath _propertyPath;
//
// 	private int? _savingInputValue;
//
//
//
// 	public EditableFieldVM(ReObject reObject, PropertyPath propertyPath)
// 	{
// 		_reObject = reObject;
// 		_propertyPath = propertyPath;
//
// 		Value = new VMField<int?>(_reObject.GetValue(_propertyPath));
// 		IsChanged = new VMField<bool>(false);
// 		IsSynchronizing = new VMField<bool>(false);
// 		IsSaving = new VMField<bool>(false);
//
// 		_reObject.SubscribePropertyObserver(_propertyPath, this);
// 	}
//
//
// 	public void OnEntered(string text)
// 	{
// 		var inputValue = (int) Convert.ChangeType(text, typeof(int));
//
// 		if (inputValue == Value.Value) {
// 			IsChanged.Set(false);
// 			return;
// 		}
//
// 		IsChanged.Set(true);
//
// 		Save(inputValue);
// 	}
//
//
//
// 	public void Save(T value)
// 	{
// 		IsSynchronizing.Set(true);
// 		IsSaving.Set(true);
//
// 		_savingInputValue = value;
//
// 		_reObject.Save(_propertyPath, value);
// 	}
//
//
// 	// IPropertyObserver
//
//
// 	public void OnNewValue(T newValue)
// 	{
// 		_savingInputValue = null;
// 		Value.Set(newValue);
// 		IsChanged.Set(false);
// 		IsSynchronizing.Set(false);
// 		IsSaving.Set(false);
// 	}
// }
//
//
//
// }
