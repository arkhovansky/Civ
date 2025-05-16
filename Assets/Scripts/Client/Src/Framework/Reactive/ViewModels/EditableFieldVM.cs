using System;

using UnityEngine;

using Civ.Common.ClientServerProtocol;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.Reactive.ViewModels {



public class EditableFieldVM<T> : IEditableFieldVM<T>, IPropertyObserver<T>
	where T : struct
{
	public VMField<T?> Value { get; }

	public VMField<bool> IsChanged { get; }

	// public VMField<bool> IsValid { get; }

	public VMField<bool> IsSynchronizing { get; }
	// public VMField<bool> IsLoading { get; }
	public VMField<bool> IsSaving { get; }



	private readonly ReObject _reObject;
	private readonly PropertyPath _propertyPath;

	private T? _savingInputValue;



	public EditableFieldVM(ReObject reObject, PropertyPath propertyPath)
	{
		_reObject = reObject;
		_propertyPath = propertyPath;

		Value = new VMField<T?>(_reObject.GetPropertyValue<T>(_propertyPath).Value);
		IsChanged = new VMField<bool>(false);
		IsSynchronizing = new VMField<bool>(false);
		IsSaving = new VMField<bool>(false);

		_reObject.SubscribePropertyObserver(_propertyPath, this);
	}


	public void OnChange(string textValue)
	{
		Debug.Log($"OnChange: {textValue}");

		var controlValue = (T) Convert.ChangeType(textValue, typeof(T));

		IsChanged.Set(!controlValue.Equals(Value.Value));
	}


	public void OnLostFocusChange(string textValue)
	{
		Debug.Log($"OnLostFocusChange: {textValue}");

		var controlValue = (T) Convert.ChangeType(textValue, typeof(T));

		if (controlValue.Equals(Value.Value)) {
			IsChanged.Set(false);
			return;
		}

		Value.SetWithoutNotification(controlValue);

		IsChanged.Set(true);

		Save(controlValue);
	}



	private async void Save(T value)
	{
		IsSynchronizing.Set(true);
		IsSaving.Set(true);

		_savingInputValue = value;

		await _reObject.SavePropertyValue(_propertyPath, value);

		Value.Set(_savingInputValue);
		_savingInputValue = null;
		IsChanged.Set(false);
		IsSynchronizing.Set(false);
		IsSaving.Set(false);
	}


	// IPropertyObserver


	public void OnPropertyValueChanged(PropertyValue<T> value)
	{
		Value.Set(value.Value);
		_savingInputValue = null;
		IsChanged.Set(false);
		IsSynchronizing.Set(false);
		IsSaving.Set(false);
	}
}



}
