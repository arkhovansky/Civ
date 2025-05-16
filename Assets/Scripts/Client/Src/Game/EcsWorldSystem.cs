using Civ.Common.ClientServerProtocol.Ecs;
using Civ.Common.Game.Components;
using Civ.Common.Grid;

using Civ.Client.Grid;



namespace Civ.Client.Game {



public static class EcsWorldSystem
{
	public static void PopulateWorld(IEcsWorld ecsWorld)
	{
		var terrainTypeRepository = new TerrainTypeRepository(new HexCell(HexOrientation.FlatTop));
		var terrainFactory = new TerrainFactory(terrainTypeRepository);

		var terrainTilePool = ecsWorld.GetComponentPool<TerrainTile>();
		var relativeAxialPositionPool = ecsWorld.GetComponentPool<RelativeAxialPosition>();

		terrainFactory.Create(terrainTilePool, relativeAxialPositionPool);
	}
}



}
