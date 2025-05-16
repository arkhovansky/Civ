using Civ.Common.ClientServerProtocol;

using Civ.Server.Controllers;
using Civ.Server.Ecs;
using Civ.Server.Service;



namespace Civ.Server {



public class Server
{
	private readonly MainController _mainController;

	private ClientProtocol.ClientProtocol? _clientProtocol;



	public Server()
	{
		var gameInstanceRepository = new GameInstanceRepository();

		var gameInstanceService = new GameInstanceService(gameInstanceRepository);
		var gameInstanceViewService = new GameInstanceViewService(gameInstanceRepository);

		var gameActionFactory = new GameActionFactory();

		var ecsEncoder = new EcsEncoder();

		var gameInstancesController = new GameInstancesController(gameInstanceService, gameInstanceViewService);
		var gameInstanceController = new GameInstanceController(gameInstanceRepository, gameInstanceViewService,
		                                                        gameActionFactory, ecsEncoder);

		_mainController = new MainController(gameInstancesController, gameInstanceController);
	}



	public IEndpoint ConnectClient(IEndpoint clientEndpoint)
	{
		_clientProtocol = new ClientProtocol.ClientProtocol(clientEndpoint, _mainController);

		return _clientProtocol;
	}
}



}
