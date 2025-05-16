using Cysharp.Threading.Tasks;

using Civ.Common.ClientServerProtocol;
using Civ.Common.ClientServerProtocol.Ecs;

using Civ.Client.Framework.Reactive;
using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.HighLevel.Impl;
using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;
using Civ.Client.Game;
using Civ.Client.Server;



namespace Civ.Client.UI.GameInstance.RunningGame {



public class RunningGameController : Controller
{
	private readonly ReObject _game;

	private readonly UniTask _startGameTask;

	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;
	private readonly ICommandRouter _commandRouter;
	private readonly IServerProtocol _serverProtocol;

	private GameView? _uiView;



	public RunningGameController(ReObject game, UniTask startGameTask,
	                             IGui gui, IVvmBinder vvmBinder, ICommandRouter commandRouter, IServerProtocol serverProtocol)
		: base(commandRouter)
	{
		_game = game;
		_startGameTask = startGameTask;

		_gui = gui;
		_vvmBinder = vvmBinder;
		_commandRouter = commandRouter;
		_serverProtocol = serverProtocol;
	}


	public override async void Start()
	{
		await _startGameTask;

		var worldSubObjectId = new SubObjectId(_game.ObjectId, new ISubObjectId[] { new SubObjectName("World") });
		var request = new Request(worldSubObjectId, "Get");
		var ecsWorld = await _serverProtocol.ExecuteRequest<IEcsWorld>(request);

		EcsWorldSystem.PopulateWorld(ecsWorld);

		var viewModel = new GameVM(_game, this,
		                           _commandRouter);
		_uiView = new GameView(viewModel,
		                       _gui, _vvmBinder);
		_gui.AddView(_uiView);
	}
}



}
