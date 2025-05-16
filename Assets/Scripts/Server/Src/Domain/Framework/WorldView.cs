using System.Collections.Generic;

using Leopotam.EcsLite;

using Civ.Common.Grid;



namespace Civ.Server.Domain.FrameWork {



public class WorldView
{
	public EcsWorld EcsWorld { get; }

	public HexGrid TerrainGrid { get; }

	// public List<NationView> GameSides { get; }

	public uint Turn { get; }
}



// public class TerrainView
// {
// 	public HexGrid Grid;
//
// 	public List<uint> TileTerrainTypes;
// }



public class NationView
{

}



}
