using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using Unity.Entities;
using Unity.Entities.Graphics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

using Civ.Common.ClientServerProtocol.Ecs;
using Civ.Common.Grid;

using Civ.Client.Game.Ecs.Components;
using Civ.Client.Grid;
using Civ.Client.Util;



namespace Civ.Client {



public class TerrainFactory
{
	private readonly ITerrainTypeRepository _terrainTypeRepository;



	public TerrainFactory(ITerrainTypeRepository terrainTypeRepository)
	{
		_terrainTypeRepository = terrainTypeRepository;
	}



	public void Create(IComponentPool<Civ.Common.Game.Components.TerrainTile> terrainTilePool,
	                   IComponentPool<Civ.Common.Game.Components.RelativeAxialPosition> relativeAxialPositionPool)
	{
		var (renderMeshArray, terrainType_To_MaterialMeshInfo) = PrepareMeshMaterialData(terrainTilePool);

		var grid = new VisualAbstractHexGrid(HexOrientation.FlatTop);


		var world = World.DefaultGameObjectInjectionWorld;
		var entityManager = world.EntityManager;

		var filterSettings = RenderFilterSettings.Default;
		filterSettings.ShadowCastingMode = ShadowCastingMode.Off;
		filterSettings.ReceiveShadows = false;

		var renderMeshDescription = new RenderMeshDescription {
			FilterSettings = filterSettings,
			LightProbeUsage = LightProbeUsage.Off
		};


		// var terrain = entityManager.CreateEntity();
		// {
		// 	var terrainMesh = new Mesh(); /*{
		// 		vertices = new Vector3[] {
		// 			new(-1, -1),
		// 			new(-1, 2),
		// 			new(2, 2),
		// 			new(2, -1),
		// 		},
		// 		normals = new Vector3[] {
		// 			new(0, 0, -1),
		// 			new(0, 0, -1),
		// 			new(0, 0, -1),
		// 			new(0, 0, -1)
		// 		},
		// 		triangles = new[] { 0, 1, 3, 1, 2, 3 }
		// 	};*/
		// 	var renderMeshArray_ = new RenderMeshArray(
		// 		new[] { Material1 },
		// 		new[] { terrainMesh });
		// 	RenderMeshUtility.AddComponents(
		// 		terrain,
		// 		entityManager,
		// 		renderMeshDescription,
		// 		renderMeshArray_,
		// 		MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));
		// 	entityManager.SetComponentData(terrain, new LocalToWorld {
		// 		Value = float4x4.TRS(
		// 			new float3(2, 0, 0),
		// 			quaternion.identity,
		// 			new float3(1))
  //           });
  //
		// 	var terrainBounds = new RenderBounds { Value = terrainMesh.bounds.ToAABB() };
		// 	entityManager.SetComponentData(terrain, terrainBounds);
		// }

		// var ecb = new EntityCommandBuffer(Allocator.TempJob);

		var prototype = entityManager.CreateEntity();
		entityManager.AddComponent<TerrainTile>(prototype);
		entityManager.AddComponent<RelativeAxialPosition>(prototype);
		// entityManager.AddComponentData(prototype, new Parent { Value = terrain });
		entityManager.AddComponent<LocalTransform>(prototype);
		RenderMeshUtility.AddComponents(
			prototype,
			entityManager,
			renderMeshDescription,
			renderMeshArray,
			MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));
		// entityManager.AddComponent<MaterialColor>(prototype);
		entityManager.AddComponent<Static>(prototype);
		// entityManager.SetComponentData(prototype, new LocalToWorld { Value = float4x4.identity });

		// var bounds = new RenderBounds { Value = tileMesh.bounds.ToAABB() };

		var terrainTileCount = terrainTilePool.Count;

		var clonedEntities = new NativeArray<Entity>((int)terrainTileCount, Allocator.Temp);
		entityManager.Instantiate(prototype, clonedEntities);


		for (uint i = 0; i < terrainTileCount; ++i) {
			var entity = clonedEntities[(int)i];

			var terrainTile = terrainTilePool.GetByIndex(i);
			var inEntity = terrainTilePool.GetEntityByIndex(i);

			var relativeAxialPosition = relativeAxialPositionPool.Get(inEntity);

			entityManager.SetComponentData(entity, new TerrainTile(terrainTile));
			entityManager.SetComponentData(entity, new RelativeAxialPosition(relativeAxialPosition));

			entityManager.SetComponentData(entity, grid.GetCellLocalTransform(relativeAxialPosition.Position));

			entityManager.SetComponentData(entity, terrainType_To_MaterialMeshInfo[terrainTile.TerrainType]);

			// float3 hsvColor = map.Tile(i).TerrainTypeId switch {
			// 	3 => new float3(130, 80, 80),
			// 	4 => new float3(70, 80, 80),
			// 	5 => new float3(80, 80, 80),
			// 	_ => new float3()
			// };
			// var rgb = Color.HSVToRGB(hsvColor.x, hsvColor.y, hsvColor.z);
			// var color = new float4(rgb.r, rgb.g, rgb.b, 1);
			// entityManager.SetComponentData(entity, new MaterialColor { Value = color });

			// entityManager.SetComponentData(entity, bounds);
		}

		clonedEntities.Dispose();


		// Spawn most of the entities in a Burst job by cloning a pre-created prototype entity,
		// which can be either a Prefab or an entity created at run time like in this sample.
		// This is the fastest and most efficient way to create entities at run time.
		// var spawnJob = new SpawnJob {
		// 	Prototype = prototype,
		// 	Map = map,
		// 	Ecb = ecb.AsParallelWriter(),
		// 	MeshBounds = bounds,
		// 	//ObjectScale = ObjectScale,
		// };
		//
		// var spawnHandle = spawnJob.Schedule((int) map.TileCount, 128);
		// bounds.Dispose(spawnHandle);
		//
		// spawnHandle.Complete();
		//
		// ecb.Playback(entityManager);
		// ecb.Dispose();

		entityManager.DestroyEntity(prototype);
	}



	//----------------------------------------------------------------------------------------------
	// private


	// [GenerateTestsForBurstCompatibility]
	// private struct SpawnJob : IJobParallelFor
	// {
	// 	public Entity Prototype;
	// 	public ITerrainMap Map;
	// 	//public float ObjectScale;
	// 	public EntityCommandBuffer.ParallelWriter Ecb;
	//
	// 	[ReadOnly] public NativeArray<RenderBounds> MeshBounds;
	//
	//
	// 	public void Execute(int index)
	// 	{
	// 		var e = Ecb.Instantiate(index, Prototype);
	//
	// 		Ecb.SetComponent(index, e, Map.Tile((uint) index));
	//
	// 		Ecb.SetComponent(index, e, new LocalToWorld { Value = Map.Grid.GetCellLocalTransform((uint) index) });
	// 		//Ecb.SetComponent(index, e, new MaterialColor() { Value = ComputeColor(index) });
	// 		// MeshBounds must be set according to the actual mesh for culling to work.
	// 		int materialIndex = (int) Map.Tile((uint) index).TerrainTypeId;
	// 		Ecb.SetComponent(index, e, MaterialMeshInfo.FromRenderMeshArrayIndices(materialIndex, 0));
	// 		Ecb.SetComponent(index, e, MeshBounds[0]);
	// 	}
	// }



	private (RenderMeshArray, Dictionary<uint, MaterialMeshInfo>)
		PrepareMeshMaterialData(IComponentPool<Civ.Common.Game.Components.TerrainTile> terrainTilePool)
	{
		var terrainTypeIds = GetTerrainTypes(terrainTilePool);

		var terrainType_To_MaterialMeshInfo = new Dictionary<uint, MaterialMeshInfo>();
		var meshes = new SetList<Mesh>();
		var materials = new SetList<Material>();

		foreach (var terrainTypeId in terrainTypeIds) {
			var terrainType = _terrainTypeRepository.Get(terrainTypeId);

			var meshIndex = meshes.Add(terrainType.Mesh);
			var materialIndex = materials.Add(terrainType.Material);

			terrainType_To_MaterialMeshInfo[terrainTypeId] =
				MaterialMeshInfo.FromRenderMeshArrayIndices(materialIndex, meshIndex);
		}

		var renderMeshArray = new RenderMeshArray(materials.ToArray(), meshes.ToArray());

		return (renderMeshArray, terrainType_To_MaterialMeshInfo);
	}


	private static HashSet<uint> GetTerrainTypes(IComponentPool<Civ.Common.Game.Components.TerrainTile> terrainTilePool)
	{
		var terrainTypes = new HashSet<uint>();

		for (uint i = 0; i < terrainTilePool.Count; ++i)
			terrainTypes.Add(terrainTilePool.GetByIndex(i).TerrainType);

		return terrainTypes;
	}

}



}
