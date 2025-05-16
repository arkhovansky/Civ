using System;

using Civ.Common.Grid;



namespace Civ.Server.Domain.FrameWork {



public class GameSide : IGameSide
{
	public Guid Id { get; }

	public AxialPosition OriginPosition { get; }

	public WorldView WorldView { get; }



	public GameSide(AxialPosition originPosition)
	{
		Id = Guid.NewGuid();
		OriginPosition = originPosition;
	}
}



}
