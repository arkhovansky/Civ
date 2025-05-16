using UnityEngine.UIElements;

// using Sodium.Frp;

using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.UnityUICore.Mvvm {



public static class ButtonExtensions
{
	public static void BindCommand(this Button button, ICommand command)
	{
		// command.Enabled.ListenWeak(button.SetEnabled);

		button.RegisterCallback<ClickEvent>(_ => command.Execute());
	}
}



}
