using System;

using Civ.Common.Grid;



namespace Civ.Server.Domain.FrameWork {



public interface IGameSide
{
	Guid Id { get; }

	public AxialPosition OriginPosition { get; }

	public WorldView WorldView { get; }
}



}
