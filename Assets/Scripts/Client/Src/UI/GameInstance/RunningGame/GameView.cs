using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.UI.GameInstance.RunningGame {



public class GameView : IView
{
	private readonly GameVM _viewModel;

	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;



	public GameView(GameVM viewModel,
                    IGui gui, IVvmBinder vvmBinder)
	{
		_viewModel = viewModel;

		_gui = gui;
		_vvmBinder = vvmBinder;
	}


	public void Build()
	{
		var visualNode = _gui.RootVisualNode;

		_gui.SetVisualResource(visualNode!, "Game");

		_vvmBinder.Bind(visualNode!, _viewModel);
	}
}



}
