using Unity.Entities;



namespace Civ.Client.Game.Ecs.Components {



public struct TerrainTile : IComponentData
{
	public uint TerrainType;



	public TerrainTile(Civ.Common.Game.Components.TerrainTile apiTerrainTile)
	{
		TerrainType = apiTerrainTile.TerrainType;
	}
}



}
