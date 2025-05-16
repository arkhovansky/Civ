// using System;
//
// using Civ.Common.Game;
//
// using Civ.Server.Domain.Game;
//
//
//
// namespace Civ.Server.Service {
//
//
//
// public class GameMasterService
// {
// 	private readonly GameInstanceRepository _gameInstanceRepository;
//
//
//
// 	public GameMasterService(GameInstanceRepository gameInstanceRepository)
// 	{
// 		_gameInstanceRepository = gameInstanceRepository;
// 	}
//
//
//
// 	public void EndGameSideTurn(Guid gameInstanceId, Guid participantId)
// 	{
// 		var gameMaster = GetGameMaster(gameInstanceId);
//
// 		gameMaster.EndGameSideTurn(participantId);
// 	}
//
//
// 	public void OnGameActionRequest
//
//
//
// 	private IGameMaster GetGameMaster(Guid gameInstanceId)
// 	{
// 		var gameInstance = _gameInstanceRepository.Get(gameInstanceId);
//
// 		if (gameInstance.Phase != GamePhase.Started)
// 			throw new InvalidOperationException();
//
// 		return gameInstance.GameMaster!;
// 	}
// }
//
//
//
// }
