using System.Collections.Generic;

using Leopotam.EcsLite;

using Civ.Common.Game;
using Civ.Common.Grid;

using Civ.Server.Domain.FrameWork;
using Civ.Server.Domain.Game.Systems;



namespace Civ.Server.Domain.WorldGenerator {



public class WorldGenerator : IWorldGenerator
{
	public World Create(WorldSpecification spec)
	{
		var ecsWorld = new EcsWorld();

		var terrainGrid = CreateTerrain(spec.Terrain, ecsWorld);

		var nations = CreateNations(spec.Polities, ecsWorld);

		return new World(ecsWorld, terrainGrid, nations);
	}


	//----------------------------------------------------------------------------------------------
	// private


	private HexGrid CreateTerrain(TerrainSpecification spec, EcsWorld ecsWorld)
	{
		var terrainGenerator = new HexTerrainGenerator();
		var hexGrid = terrainGenerator.Generate(spec.Width, spec.Height,
		                                        HexOrientation.FlatTop, HexGridLineOffset.Odd,
		                                        ecsWorld);
		return hexGrid;
	}



	private List<IGameSide> CreateNations(IReadOnlyList<PolitySpecification> spec, EcsWorld ecsWorld)
	{
		var nations = new List<IGameSide>(spec.Count);

		var origins = new AxialPosition[] {
			new(1, 1),
			new(3, -1)
		};

		for (uint i = 0; i < spec.Count; ++i) {
			nations.Add(new GameSide(origins[i]));

			var nationEntity = ecsWorld.NewEntity();
			var nationPool = ecsWorld.GetPool<EcsNation>();
			ref var nationComponent = ref nationPool.Add(nationEntity);
			nationComponent = new EcsNation { Index = i };
		}

		return nations;
	}
}



}
