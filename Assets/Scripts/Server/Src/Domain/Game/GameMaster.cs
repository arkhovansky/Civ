using System;

using Civ.Server.Domain.FrameWork;



namespace Civ.Server.Domain.Game {



public class GameMaster : IGameMaster
{
	public uint? CurrentTurnGameSideIndex { get; private set; }



	private readonly GameInstance _game;
	private readonly World _world;



	public GameMaster(GameInstance game)
	{
		_game = game;
		_world = game.World!;
	}



	public void Start()
	{
		StartNextTurn();
	}


	public void EndGameSideTurn(Guid gameSideId)
	{
		//TODO: Check gameSide == current

		if (CurrentTurnGameSideIndex < _world.GameSides.Count - 1)
			++CurrentTurnGameSideIndex;
		else
			StartNextTurn();
	}


	public void OnGameAction(IGameAction action, Guid gameSideId)
	{
		ValidateAction(action, gameSideId);

		ProcessAction(action);
	}



	private void StartNextTurn()
	{
		_world.Turn += 1;

		CurrentTurnGameSideIndex = 0;
	}



	private void ValidateAction(IGameAction action, Guid gameSideId)
	{
		var gameSideIndex = _world.GetGameSideIndex(gameSideId);

		if (gameSideIndex != CurrentTurnGameSideIndex)
			throw new InvalidOperationException();
	}


	private void ProcessAction(IGameAction action)
	{
		// action.Validate(_world);
		// action.PlaceToWorld(_world);
		//
		// _systems.Run();
	}
}



}
