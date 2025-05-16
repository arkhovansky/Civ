using Civ.Common.ClientServerProtocol;

using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.HighLevel.Impl;
using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;
using Civ.Client.Server;
using Civ.Client.UI.GameInstance;



namespace Civ.Client.UI.MainMenu {



public class MainMenuController : Controller
{
	private readonly IVisualNodePath _visualNodePath;

	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;
	private readonly ICommandRouter _commandRouter;
	private readonly IServerProtocol _serverProtocol;



	public MainMenuController(IVisualNodePath visualNodePath,
	                          IGui gui, IVvmBinder vvmBinder, ICommandRouter commandRouter, IServerProtocol serverProtocol)
		: base(commandRouter)
	{
		_visualNodePath = visualNodePath;

		_gui = gui;
		_vvmBinder = vvmBinder;
		_commandRouter = commandRouter;
		_serverProtocol = serverProtocol;

		base.AddCommandHandler<NewGameCommand>(OnNewGameCommand);

		var viewModel = new MainMenuVM(this,
		                               _commandRouter);
		var view = new MainMenuView(visualNodePath, viewModel,
		                            _gui, _vvmBinder);
		_gui.AddView(view);
	}


	public override void Start()
	{
	}


	private void OnNewGameCommand(NewGameCommand command)
	{
		var request = new Request(new RootObjectId("GameInstances"), "CreateThenGet");
		var gameInstanceTask = _serverProtocol.ExecuteRequest<CreateThenGetResult>(request);

		var childController = new GameInstanceController(gameInstanceTask, _visualNodePath,
		                                                 _gui, _vvmBinder, _commandRouter, _serverProtocol);
		AddChildController(childController);
		childController.Start();
	}
}



}
