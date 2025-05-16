// using UnityEngine;
//
//
//
// namespace Civ {
//
//
//
// public class ProceduralHexTerrainTileFactory : ITerrainTileFactory
// {
// 	public ProceduralHexTerrainTileFactory(HexCell cell)
// 	{
// 		_flatMesh = CreateFlatTileMesh();
// 		_hillsMesh = CreateHillsMesh();
// 	}
//
//
// 	public Mesh GetMesh(uint terrainTypeId)
// 	{
// 		return terrainTypeId switch {
// 			0 => _flatMesh,
// 			1 => _flatMesh,
// 			2 =>
// 		}
// 	}
//
// 	public Material GetMaterial(uint terrainTypeId);
//
//
// 	//----------------------------------------------------------------------------------------------
// 	// private
//
// 	private readonly Mesh _flatMesh;
// 	private readonly Mesh _hillsMesh;
// }
//
//
//
// }
