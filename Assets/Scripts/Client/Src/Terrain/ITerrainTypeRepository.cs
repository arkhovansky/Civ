using UnityEngine;



namespace Civ.Client {



public class TerrainType
{
	// public uint Id;
	public Mesh Mesh;
	public Material Material;
}



public interface ITerrainTypeRepository
{
	TerrainType Get(uint terrainTypeId);
}



}
