using Civ.Client.Framework.Reactive;
using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.Mvvm;

using ICommand = Civ.Client.Framework.UICore.Mvvm.ICommand;



namespace Civ.Client.UI.GameInstance.RunningGame {



public class GameVM : IViewModel
{
	// public VMField<uint> Turn { get; }

	public ICommand EndTurnCommand { get; }



	public GameVM(ReObject game, IController controller,
	              ICommandRouter commandRouter)
	{
		EndTurnCommand = new Command(() => commandRouter.EmitCommand(new EndTurnCommand(), controller));
	}
}



}
