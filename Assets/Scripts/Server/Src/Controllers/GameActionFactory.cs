using System;

using Civ.Server.Domain.FrameWork;

using Civ.Server.Domain.Game.Actions;



namespace Civ.Server.Controllers {



public class GameActionFactory
{
	public IGameAction Create(object actionDto)
	{
		switch (actionDto) {
			// case MoveUnitDto dto: return new MoveUnit(dto);

			default:
				throw new ArgumentException();
		}
	}
}



}
