using System.Collections.Generic;

using UnityEngine;

using Civ.Client.Grid;



namespace Civ.Client {



public class TerrainTypeRepository : ITerrainTypeRepository
{
	public TerrainTypeRepository(HexCell cell)
	{
		_cell = cell;

		var flatMesh = CreateFlatTileMesh();

		var lakeMaterial = Resources.Load<Material>("Materials/Terrain/Lake");
		var oceanMaterial = Resources.Load<Material>("Materials/Terrain/Ocean");
		var deepOceanMaterial = Resources.Load<Material>("Materials/Terrain/DeepOcean");
		var grasslandsMaterial = Resources.Load<Material>("Materials/Terrain/Grasslands");
		var plainsMaterial = Resources.Load<Material>("Materials/Terrain/Plains");
		var hillsMaterial = Resources.Load<Material>("Materials/Terrain/Hills");
		var mountainsMaterial = Resources.Load<Material>("Materials/Terrain/Mountains");


		_terrainTypes[0] = new TerrainType { /*Id = 0,*/ Mesh = flatMesh, Material = lakeMaterial };
		_terrainTypes[1] = new TerrainType { /*Id = 1,*/ Mesh = flatMesh, Material = oceanMaterial };
		_terrainTypes[2] = new TerrainType { /*Id = 2,*/ Mesh = flatMesh, Material = deepOceanMaterial };
		_terrainTypes[3] = new TerrainType { /*Id = 3,*/ Mesh = flatMesh, Material = grasslandsMaterial };
		_terrainTypes[4] = new TerrainType { /*Id = 4,*/ Mesh = flatMesh, Material = plainsMaterial };
		_terrainTypes[5] = new TerrainType { /*Id = 5,*/ Mesh = CreateHillsMesh(), Material = hillsMaterial };
		_terrainTypes[6] = new TerrainType { /*Id = 6,*/ Mesh = CreateMountainsMesh(), Material = mountainsMaterial };
	}


	public TerrainType Get(uint terrainTypeId)
	{
		return _terrainTypes[terrainTypeId];
	}


	//----------------------------------------------------------------------------------------------
	// private

	private readonly HexCell _cell;

	private readonly Dictionary<uint, TerrainType> _terrainTypes = new();


	//----------------------------------------------------------------------------------------------


	private Mesh CreateFlatTileMesh()
	{
		return _cell.GetMesh();
	}


	private Mesh CreateHillsMesh()
	{
		return CreateElevationsMesh(0.07f);
	}

	private Mesh CreateMountainsMesh()
	{
		return CreateElevationsMesh(0.2f);
	}


	private Mesh CreateElevationsMesh(float elevationHeight)
	{
		IReadOnlyList<Vector3> borderVertices = _cell.GetBorderVertices();
		Vector3 center = _cell.GetCenter();

		var vertices = new Vector3[6 * 3 * 3];
		var triangles = new int[6 * 3 * 3];

		for (int fragment = 0; fragment < 6; ++fragment) {
			var borderVertex2Index = fragment < 5 ? fragment + 1 : 0;

			CreateElevation(new [] { borderVertices[fragment], borderVertices[borderVertex2Index], center },
			                elevationHeight,
			                vertices, triangles,
				            fragment);
		}

		var mesh = new Mesh { vertices = vertices, triangles = triangles };
		mesh.RecalculateNormals();

		return mesh;
	}


	private void CreateElevation(Vector3[] baseVertices, float height,
	                             Vector3[] meshVertices, int[] meshTriangles,
	                             int fragment)
	{
		var topX = (baseVertices[0].x + baseVertices[1].x + baseVertices[2].x) / 3;
		var topZ = (baseVertices[0].z + baseVertices[1].z + baseVertices[2].z) / 3;
		var top = new Vector3(topX, height, topZ);

		var verticesIndex = fragment * 3 * 3;
		var trianglesIndex = fragment * 3 * 3;

		for (uint face = 0; face < baseVertices.Length; ++face) {
			meshVertices[verticesIndex] = baseVertices[face];
			var index2 = face < baseVertices.Length - 1 ? face + 1 : 0;
			meshVertices[verticesIndex + 1] = baseVertices[index2];
			meshVertices[verticesIndex + 2] = top;

			meshTriangles[trianglesIndex++] = verticesIndex;
			meshTriangles[trianglesIndex++] = verticesIndex + 1;
			meshTriangles[trianglesIndex++] = verticesIndex + 2;

			verticesIndex += 3;
		}
	}
}



}
