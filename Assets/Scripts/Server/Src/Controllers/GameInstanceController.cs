using System;
using System.Linq;
using System.Reflection;

using Civ.Common.ClientServerProtocol;

using Civ.Server.Ecs;
using Civ.Server.Service;



namespace Civ.Server.Controllers {



public class GameInstanceController
{
	private readonly GameInstanceRepository _gameInstanceRepository;
	private readonly GameInstanceViewService _gameInstanceViewService;
	private readonly GameActionFactory _gameActionFactory;
	private readonly IEcsEncoder _ecsEncoder;



	public GameInstanceController(GameInstanceRepository gameInstanceRepository,
	                              GameInstanceViewService gameInstanceViewService,
	                              GameActionFactory gameActionFactory,
	                              IEcsEncoder ecsEncoder)
	{
		_gameInstanceRepository = gameInstanceRepository;
		_gameInstanceViewService = gameInstanceViewService;
		_gameActionFactory = gameActionFactory;
		_ecsEncoder = ecsEncoder;
	}



	public Reply HandleRequest(UserRequest userRequest)
	{
		var request = userRequest.Request;

		switch (request.ObjectId) {
			case RootObjectId rootObjectId: return HandleGameInstanceRequest(request);
			case SubObjectId subObjectId: return HandleSubObjectRequest(request);

			default:
				throw new ArgumentOutOfRangeException();
		}
	}



	private Reply HandleGameInstanceRequest(Request request)
	{
		var gameInstanceId = ((RootObjectId)request.ObjectId).Id!.Value;
		var gameInstance = _gameInstanceRepository.Get(gameInstanceId);

		switch (request.Operation) {
			case "Update":
				UpdateObject(gameInstance, (ObjectUpdateData)request.Data!);

				break;

			// case "GenerateWorld":
			// 	gameInstance.GenerateWorld();
			// 	break;

			case "Start":
				gameInstance.GenerateWorld();
				gameInstance.Start();

				break;

			default:
				throw new NotImplementedException();
		}

		return new Reply(ResultCode.Ok);
	}



	private Reply HandleSubObjectRequest(Request request)
	{
		var subObjectId = (SubObjectId) request.ObjectId;
		var gameInstanceId = subObjectId.RootObjectId.Id!.Value;
		var subObjectPath = subObjectId.SubObjectPath;

		if (!(subObjectPath.Length == 1 && subObjectPath[0] is SubObjectName))
			throw new ArgumentOutOfRangeException();

		var subObjectName = ((SubObjectName)subObjectPath[0]).Name;

		switch (subObjectName) {
			case "World":
				switch (request.Operation) {
					case "Get": {
						var ecsWorld = _gameInstanceViewService.GetWorldView(gameInstanceId, Guid.NewGuid());
						var encodedEcsWorld = _ecsEncoder.Encode(ecsWorld);

						return new Reply(ResultCode.Ok, new GetResult(encodedEcsWorld));
					}
					break;

					default:
						throw new NotImplementedException();
				}
				break;

			case "GameMaster": {
				var gameInstance = _gameInstanceRepository.Get(gameInstanceId);

				switch (request.Operation) {
					case "Get": {
						throw new NotImplementedException();

					}
					break;

					case "EndTurn":
						gameInstance.GetGameMaster().EndGameSideTurn(Guid.NewGuid());

						return new Reply(ResultCode.Ok);

					case "GameAction": {
						var action = _gameActionFactory.Create(request.Data!);
						gameInstance.GetGameMaster().OnGameAction(action, Guid.NewGuid());

						return new Reply(ResultCode.Ok);
					}

					default:
						throw new NotImplementedException();
				}
			}
			break;

			default:
				throw new ArgumentOutOfRangeException();
		}

	}



	private void UpdateObject(object @object, ObjectUpdateData updateData)
	{
		UpdateObjectProperty(@object, updateData.PropertyPath, updateData.Value);
	}


	private void UpdateObjectProperty(object @object, PropertyPath propertyPath, object value)
	{
		var segments = propertyPath.Segments;

		for (var i = 0; ;) {
			var type = @object.GetType();
			var segment = segments[i];

			switch (segment) {
				case string name: {
					var property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);

					if (++i == segments.Length) {
						property.SetValue(@object, value);

						return;
					}

					@object = property.GetValue(@object);
				}
				break;

				case int:
				case uint: {
					var index = (uint)segment;
					var indexerProperty = type.GetProperty("Item");

					if (++i == segments.Length) {
						indexerProperty.SetValue(@object, value, new object[] { index });;

						return;
					}

					@object = indexerProperty.GetValue(@object, new object[] { index });
				}
				break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}



}
