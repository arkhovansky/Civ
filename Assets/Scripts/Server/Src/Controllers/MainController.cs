using System;

using Civ.Common.ClientServerProtocol;



namespace Civ.Server.Controllers {



public class MainController
{
	private readonly GameInstancesController _gameInstancesController;
	private readonly GameInstanceController _gameInstanceController;



	public MainController(GameInstancesController gameInstancesController,
	                      GameInstanceController gameInstanceController)
	{
		_gameInstancesController = gameInstancesController;
		_gameInstanceController = gameInstanceController;
	}


	public Reply HandleRequest(UserRequest userRequest)
	{
		Reply reply;

		var request = userRequest.Request;

		if (request.ObjectId is RootObjectId { Type: "GameInstances" }) {
			switch (request.Operation) {
				case "CreateThenGet":
					reply = _gameInstancesController.CreateThenGet();
					break;

				default:
					throw new NotImplementedException();
			}
		}
		else if (request.ObjectId is RootObjectId { Type: "GameInstance" }
		                          or SubObjectId { RootObjectId: { Type: "GameInstance" } })
		{
			reply = _gameInstanceController.HandleRequest(userRequest);
		}
		else
			throw new NotImplementedException();

		return reply;
	}
}



}
