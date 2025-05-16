using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.HighLevel.Impl;
using Civ.Client.Framework.UICore.Mvvm;

using ICommand = Civ.Client.Framework.UICore.Mvvm.ICommand;



namespace Civ.Client.UI.MainMenu
{
	public class MainMenuVM : IViewModel
	{
		public ICommand NewGameCommand { get; }

		public ICommand LoadGameCommand { get; }

		public ICommand ExitCommand { get; }



		public MainMenuVM(IController controller,
		                  ICommandRouter commandRouter)
		{
			NewGameCommand = new Command(() => commandRouter.EmitCommand(new NewGameCommand(), controller));
			LoadGameCommand = new Command(() => commandRouter.EmitCommand(new GoToLoadGameDialogCommand(), controller));
			ExitCommand = new Command(() => commandRouter.EmitCommand(new ExitApplicationCommand(), controller));
		}
	}
}
