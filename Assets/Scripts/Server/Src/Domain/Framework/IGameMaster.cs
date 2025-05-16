using System;



namespace Civ.Server.Domain.FrameWork {



public interface IGameMaster
{
	void Start();

	void EndGameSideTurn(Guid gameSideId);

	void OnGameAction(IGameAction action, Guid gameSideId);
}



}
