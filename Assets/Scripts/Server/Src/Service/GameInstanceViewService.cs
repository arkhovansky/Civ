using System;

using Leopotam.EcsLite;

using Civ.Common.Game;



namespace Civ.Server.Service {



public class GameInstanceViewService
{
	private readonly GameInstanceRepository _repository;



	public GameInstanceViewService(GameInstanceRepository repository)
	{
		_repository = repository;
	}



	public GameInstanceView GetGameInstanceView(Guid gameId, Guid participantId)
	{
		var gameInstance = _repository.Get(gameId);

		var view = new GameInstanceView {
			Id = gameInstance.Id,
			Phase = gameInstance.Phase,
			Specification = gameInstance.Specification
		};

		return view;
	}



	public EcsWorld GetWorldView(Guid gameId, Guid participantId)
	{
		var gameInstance = _repository.Get(gameId);

		if (gameInstance.Phase != GamePhase.Started)
			throw new InvalidOperationException();

		var internalWorldView = gameInstance.World!.GameSides[0].WorldView;

		return internalWorldView.EcsWorld;
	}
}



}
