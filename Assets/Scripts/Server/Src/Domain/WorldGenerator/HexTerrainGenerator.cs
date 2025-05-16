using Leopotam.EcsLite;

using Civ.Common.Game.Components;
using Civ.Common.Grid;

using Civ.Server.Domain.Game.Systems;



namespace Civ.Server.Domain.WorldGenerator {



public class HexTerrainGenerator : IHexTerrainGenerator
{
	public HexGrid Generate(uint width, uint height,
	                        HexOrientation orientation, HexGridLineOffset lineOffset,
	                        EcsWorld ecsWorld)
	{
		var grid = new HexGrid(width, height, orientation, lineOffset);

		var tiles = new uint[] {
			3, 4, 5, 6,
			0, 3, 4, 1,
			1, 1, 1, 2
		};


		var positionPool = ecsWorld.GetPool<Position>();
		var terrainTilePool = ecsWorld.GetPool<TerrainTile>();

		for (uint y = 0; y < height; ++y) {
			for (uint x = 0; x < width; ++x) {
				var tileIndex = x * y;

				var tileEntity = ecsWorld.NewEntity();

				ref var position = ref positionPool.Add(tileEntity);
				position.Axial = grid.AxialPositionFromCellIndex(tileIndex);

				ref var terrainTile = ref terrainTilePool.Add(tileEntity);
				terrainTile.TerrainType = tiles[tileIndex];
			}
		}

		return grid;
	}
}



}
