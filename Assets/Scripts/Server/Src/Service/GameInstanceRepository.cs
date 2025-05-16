using System;
using System.Collections.Generic;

using Civ.Server.Domain.FrameWork;



namespace Civ.Server.Service {



public class GameInstanceRepository
{
	private List<GameInstance_ReObject> _gameInstances = new();


	public GameInstance_ReObject Create()
	{
		var gameInstance = new GameInstance_ReObject();
		_gameInstances.Add(gameInstance);

		return gameInstance;
	}


	public GameInstance_ReObject Get(Guid id)
	{
		return _gameInstances.Find(it => it.Id == id);
	}
}



}
