using System;

using Civ.Common.Game;

using Civ.Server.Domain.FrameWork;



namespace Civ.Server.Service {



public class GameInstanceService
{
	private readonly GameInstanceRepository _repository;



	public GameInstanceService(GameInstanceRepository repository)
	{
		_repository = repository;
	}


	public Guid CreateGameInstance()
	{
		var gameInstance = _repository.Create();

		return gameInstance.Id;
	}



	public void InitializeGameSpecification(Guid gameInstanceId)
	{
		var gameInstance = _repository.Get(gameInstanceId);
		var game = gameInstance.Specification;

		var world = game.World;

		var terrain = world.Terrain;
		terrain.Width = 4;
		terrain.Height = 3;


		var gameSides = game.GameSides;

		var gameSide = gameSides.AddDefault();
		var polity = gameSide.Polity;
		polity.NationKind.Set(MaybeRandom.Random);
		gameSide.Player.Set(new HumanPlayerSpecification());

		gameSide = gameSides.AddDefault();
		polity = gameSide.Polity;
		polity.NationKind.Set(MaybeRandom.Random);
		gameSide.Player.Set(new AiPlayerSpecification());
	}
}



}
