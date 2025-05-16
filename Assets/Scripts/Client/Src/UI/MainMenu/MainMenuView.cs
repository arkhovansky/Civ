using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.UI.MainMenu {



public class MainMenuView : IView
{
	private readonly IVisualNodePath _visualNodePath;
	private readonly MainMenuVM _viewModel;

	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;



	public MainMenuView(IVisualNodePath visualNodePath, MainMenuVM viewModel,
	                    IGui gui, IVvmBinder vvmBinder)
	{
		_visualNodePath = visualNodePath;
		_viewModel = viewModel;

		_gui = gui;
		_vvmBinder = vvmBinder;
	}


	public void Build()
	{
		var visualNode = _gui.GetVisualNode(_visualNodePath);

		_gui.SetVisualResource(visualNode, "MainMenu");

		_vvmBinder.Bind(visualNode, _viewModel);
	}
}



}
