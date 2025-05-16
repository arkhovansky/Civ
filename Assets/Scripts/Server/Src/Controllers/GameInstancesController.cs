using System;

using Civ.Common.ClientServerProtocol;

using Civ.Server.Service;



namespace Civ.Server.Controllers {



public class GameInstancesController
{
	private readonly GameInstanceService _service;
	private readonly GameInstanceViewService _viewService;



	public GameInstancesController(GameInstanceService service, GameInstanceViewService viewService)
	{
		_service = service;
		_viewService = viewService;
	}


	public Reply CreateThenGet()
	{
		var gameId = _service.CreateGameInstance();
		_service.InitializeGameSpecification(gameId);

		var gameView = _viewService.GetGameInstanceView(gameId, Guid.NewGuid() /*participantId*/);

		var id = new RootObjectId("GameInstance", gameId);
		return new Reply(new CreateThenGetResult(id, gameView));
	}
}



}
