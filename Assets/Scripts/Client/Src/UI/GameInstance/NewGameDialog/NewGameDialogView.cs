using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.UI.GameInstance.NewGameDialog {



public class NewGameDialogView : IView
{
	private readonly IVisualNodePath _visualNodePath;
	private readonly NewGameDialogVM _viewModel;

	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;



	public NewGameDialogView(IVisualNodePath visualNodePath, NewGameDialogVM viewModel,
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

		_gui.SetVisualResource(visualNode, "NewGameDialog");

		_vvmBinder.Bind(visualNode, _viewModel);
	}
}



}
