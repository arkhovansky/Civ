using Civ.Client.Framework.Reactive;
using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.HighLevel.Impl;
using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;
using Civ.Client.Server;



namespace Civ.Client.UI.GameInstance.NewGameDialog {



public class NewGameInstanceController : Controller
{
	private readonly ReObject _gameInstance;

	private readonly IVisualNodePath _visualNodePath;

	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;
	private readonly ICommandRouter _commandRouter;
	private readonly IServerProtocol _serverProtocol;

	private IView? _view;



	public NewGameInstanceController(ReObject gameInstance,
	                                 IVisualNodePath visualNodePath,
	                                 IGui gui, IVvmBinder vvmBinder, ICommandRouter commandRouter, IServerProtocol serverProtocol)
		: base(commandRouter)
	{
		_gameInstance = gameInstance;
		_visualNodePath = visualNodePath;

		_gui = gui;
		_vvmBinder = vvmBinder;
		_commandRouter = commandRouter;
		_serverProtocol = serverProtocol;
	}


	public override async void Start()
	{
		// _nationKindDataHandle = await _database.Load("Game.NationKind[].Name");


		var viewModel = new NewGameDialogVM(_gameInstance, this,
		                                    /*_database,*/ _commandRouter);
		_view = new NewGameDialogView(_visualNodePath, viewModel,
		                              _gui, _vvmBinder);
		_gui.AddView(_view);
	}


	public override void Destroy()
	{
		_gui.RemoveView(_view!);
	}
}



}
