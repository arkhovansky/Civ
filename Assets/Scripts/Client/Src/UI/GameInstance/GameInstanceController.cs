using Cysharp.Threading.Tasks;

using Civ.Common.ClientServerProtocol;

using Civ.Client.Framework.Reactive;
using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.HighLevel.Impl;
using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;
using Civ.Client.Game;
using Civ.Client.Server;
using Civ.Client.UI.GameInstance.NewGameDialog;
using Civ.Client.UI.GameInstance.RunningGame;



namespace Civ.Client.UI.GameInstance {



public class GameInstanceController : Controller
{
	private readonly UniTask<CreateThenGetResult> _gameInstanceTask;

	private readonly IVisualNodePath _visualNodePath;

	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;
	private readonly ICommandRouter _commandRouter;
	private readonly IServerProtocol _serverProtocol;

	private ReObject? _gameInstance;

	private IController? _childController;



	public GameInstanceController(UniTask<CreateThenGetResult> gameInstanceTask,
	                              IVisualNodePath visualNodePath,
	                              IGui gui, IVvmBinder vvmBinder, ICommandRouter commandRouter, IServerProtocol serverProtocol)
		: base(commandRouter)
	{
		_gameInstanceTask = gameInstanceTask;
		_visualNodePath = visualNodePath;

		_gui = gui;
		_vvmBinder = vvmBinder;
		_commandRouter = commandRouter;
		_serverProtocol = serverProtocol;

		base.AddCommandHandler<StartGameCommand>(OnStartGameCommand);
	}


	public override async void Start()
	{
		var createThenGetResult = await _gameInstanceTask;

		_gameInstance = new GameInstanceViewReObject(
			createThenGetResult.ObjectId.Id!.Value, createThenGetResult.Object,
			_serverProtocol);

		_childController = new NewGameInstanceController(_gameInstance, _visualNodePath,
		                                                 _gui, _vvmBinder, _commandRouter, _serverProtocol);
		AddChildController(_childController);
		_childController.Start();
	}


	private void OnStartGameCommand(StartGameCommand command)
	{
		var request = new Request(_gameInstance!.ObjectId, "Start");
		var startGameTask = _serverProtocol.ExecuteRequest(request);

		var childController = new RunningGameController(_gameInstance, startGameTask,
			                                            _gui, _vvmBinder, _commandRouter, _serverProtocol);
		ReplaceChildController(_childController, childController);
		_childController = childController;
		_childController.Start();
	}
}



}
