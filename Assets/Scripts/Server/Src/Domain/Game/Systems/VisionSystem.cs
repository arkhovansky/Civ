using System.Collections.Generic;

using Leopotam.EcsLite;

using Civ.Common.Grid;

using Civ.Server.Domain.FrameWork;



namespace Civ.Server.Domain.Game.Systems {



public struct Vision
{
	public uint Radius;
}



public struct Visibility
{
	public uint Nations;


	public void SetVisibleBy(uint nationIndex) {
		Nations &= EncodeNation(nationIndex);
	}

	public bool IsVisibleBy(uint nationIndex) {
		return (Nations & EncodeNation(nationIndex)) != 0;
	}

	private uint EncodeNation(uint nationIndex) => 1u << (int)nationIndex;
}



// Used in world views
public struct Visible {}



public class VisionSystem : IEcsInitSystem, IEcsRunSystem
{
	private readonly World _world;

	private EcsFilter _visionFilter;
	private EcsFilter _positionFilter;

	private EcsPool<Vision> _visionPool;
	private EcsPool<Position> _positionPool;
	private EcsPool<OwnedBy> _ownedByPool;
	private EcsPool<EcsNation> _nationPool;
	private EcsPool<Visibility> _visiblePool;


	//----------------------------------------------------------------------------------------------


	public VisionSystem(World world)
	{
		_world = world;
	}



	public void Init(IEcsSystems systems)
	{
		var ecsWorld = systems.GetWorld();

		_visionFilter = ecsWorld.Filter<Vision>().End();
		_positionFilter = ecsWorld.Filter<Position>().End();

		_visionPool = ecsWorld.GetPool<Vision>();
		_positionPool = ecsWorld.GetPool<Position>();
		_ownedByPool = ecsWorld.GetPool<OwnedBy>();
		_nationPool = ecsWorld.GetPool<EcsNation>();
		_visiblePool = ecsWorld.GetPool<Visibility>();
	}



	public void Run(IEcsSystems systems)
	{
		var ecsWorld = systems.GetWorld();

		var visibleTilePositions_By_Nation = new SortedSet<AxialPosition>[_world.GameSides.Count];

		foreach (var visionEntity in _visionFilter) {
			var vision = _visionPool.Get(visionEntity);
			var position = _positionPool.Get(visionEntity);

			var ownedBy = _ownedByPool.Get(visionEntity);
			ownedBy.PackedEntity.Unpack(ecsWorld, out var nationEntity);
			var nationIndex = GetNationIndexByEntity(nationEntity);

			var entityVisibleTileIndices = _world.TerrainGrid.GetCellsWithinRadius(position.Axial, vision.Radius);

			visibleTilePositions_By_Nation[nationIndex].UnionWith(entityVisibleTileIndices);
		}

		foreach (var entity in _positionFilter) {
			var position = _positionPool.Get(entity);

			for (uint nationIndex = 0; nationIndex < _world.GameSides.Count; ++nationIndex) {
				if (visibleTilePositions_By_Nation[nationIndex].Contains(position.Axial)) {
					ref var visible = ref _visiblePool.Get(entity);
					visible.SetVisibleBy(nationIndex);
				}
			}
		}
	}


	//----------------------------------------------------------------------------------------------

	private uint GetNationIndexByEntity(int nationEntity)
	{
		ref var nation = ref _nationPool.Get(nationEntity);
		return nation.Index;
	}
}



}
