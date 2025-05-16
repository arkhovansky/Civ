using UnityEngine.UIElements;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.UnityUICore.Mvvm {



public static class TextElementExtensions
{
	public static void BindViewModel<T>(this TextElement control, IValueVM<T> viewModel)
	{
		control.text = ToString(viewModel.Value.Value);

		viewModel.Value.Listen(value => control.text = ToString(value));
	}


	private static string ToString<T>(T value)
	{
		return value is not null ? value.ToString() : string.Empty;
	}
}



}
