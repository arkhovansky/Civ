using UnityEngine.UIElements;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.UnityUICore.Mvvm {



public static class TextFieldExtensions
{
	public static void BindViewModel<T>(this TextField control, IEditableFieldVM<T> viewModel) where T : struct
	{
		control.value = ToString(viewModel.Value.Value);
		control.isDelayed = true;

		viewModel.Value.Listen(v => control.value = ToString(v));

		viewModel.IsChanged.Listen(it => {
			if (it) control.AddToClassList("changed"); else control.RemoveFromClassList("changed");
		});


		control.RegisterCallback<InputEvent>(evt => viewModel.OnChange(evt.newData));
		control.RegisterValueChangedCallback(evt => viewModel.OnLostFocusChange(evt.newValue));
	}


	private static string ToString<T>(T value)
	{
		return value is not null ? value.ToString() : string.Empty;
	}
}



}
