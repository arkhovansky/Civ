using System;
using System.Collections.Generic;

using Leopotam.EcsLite;

using Civ.Common.Grid;



namespace Civ.Server.Domain.FrameWork {



public class World
{
	public EcsWorld EcsWorld { get; }

	public HexGrid TerrainGrid { get; }

	public List<IGameSide> GameSides { get; }

	public uint Turn { get; set; }


	//----------------------------------------------------------------------------------------------

	public World(EcsWorld ecsWorld, HexGrid terrainGrid, List<IGameSide> gameSides)
	{
		EcsWorld = ecsWorld;
		TerrainGrid = terrainGrid;
		GameSides = gameSides;

		Turn = 1;
	}



	public uint GetGameSideIndex(Guid gameSideId)
	{
		return (uint) GameSides.FindIndex(it => it.Id == gameSideId);
	}



	// public RelativePosition GetRelativePosition(uint tileIndex, uint relativeOriginTileIndex)
	// {
	// 	return GetRelativePosition(TerrainGrid.AxialPositionFromCellIndex(tileIndex),
	// 	                           TerrainGrid.AxialPositionFromCellIndex(relativeOriginTileIndex));
	// }
}



}
