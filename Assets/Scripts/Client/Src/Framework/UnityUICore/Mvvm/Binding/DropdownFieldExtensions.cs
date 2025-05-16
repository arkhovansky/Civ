using System.Linq;

using UnityEngine.UIElements;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.UnityUICore.Mvvm {



public static class DropdownFieldExtensions
{
	public static void BindViewModel<T>(this DropdownField control, ISelectableVM<T> viewModel)
	{
		control.choices = viewModel.Choices.Select(it => ToString(it)).ToList();
		control.index = (int) viewModel.Index.Value;

		viewModel.Index.Listen(v => control.index = (int) v);

		control.RegisterCallback<InputEvent>(evt => viewModel.OnChange(evt.newData, control.index));
		control.RegisterValueChangedCallback(evt => viewModel.OnValueChanged(evt.newValue, control.index));
	}


	private static string ToString<T>(T value)
	{
		return value is not null ? value.ToString() : string.Empty;
	}
}



}
