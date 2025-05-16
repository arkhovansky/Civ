using Leopotam.EcsLite;

using Civ.Common.Game.Components;
using Civ.Common.Grid;

using Civ.Server.Domain.FrameWork;



namespace Civ.Server.Domain.Game.Systems {



public class WorldViewSystem : IEcsInitSystem, IEcsRunSystem
{
	private readonly World _world;

	private EcsFilter _visibilityFilter;

	private EcsPool<Visibility> _visibilityPool;


	//----------------------------------------------------------------------------------------------


	public WorldViewSystem(World world)
	{
		_world = world;
	}



	public void Init(IEcsSystems systems)
	{
		var ecsWorld = systems.GetWorld();
		_visibilityFilter = ecsWorld.Filter<Visibility>().End();
		_visibilityPool = ecsWorld.GetPool<Visibility>();
	}


	public void Run(IEcsSystems systems)
	{
		foreach (var visibleEntity in _visibilityFilter) {
			var visibility = _visibilityPool.Get(visibleEntity);

			for (uint nationIndex = 0; nationIndex < _world.GameSides.Count; ++nationIndex) {
				var nation = _world.GameSides[(int)nationIndex];

				if (visibility.IsVisibleBy(nationIndex)) {
					AddEntityToWorldView(systems.GetWorld(), visibleEntity,
					                     nation.WorldView.EcsWorld, nation.OriginPosition);
				}
			}
		}
	}


	//----------------------------------------------------------------------------------------------
	// private


	private void AddEntityToWorldView(EcsWorld ecsWorld, int entity,
	                                  EcsWorld viewEcsWorld, AxialPosition viewOriginPosition)
	{
		var viewEntity = viewEcsWorld.NewEntity();

		var positionPool = ecsWorld.GetPool<Position>();
		if (positionPool.Has(entity)) {
			var position = positionPool.Get(entity);

			var viewRelativePositionPool = viewEcsWorld.GetPool<RelativePosition>();
			ref var viewRelativePosition = ref viewRelativePositionPool.Add(viewEntity);
			viewRelativePosition = new RelativePosition(position.Axial - viewOriginPosition);
		}

		// CopyComponentToOtherWorld<OwnedBy>(ecsWorld, entity, viewEcsWorld, viewEntity);
		CopyComponentToOtherWorld<TerrainTile>(ecsWorld, entity, viewEcsWorld, viewEntity);
		CopyComponentToOtherWorld<Vision>(ecsWorld, entity, viewEcsWorld, viewEntity);

		var viewVisiblePool = viewEcsWorld.GetPool<Visible>();
        viewVisiblePool.Add(entity);
	}


	private void CopyComponentToOtherWorld<TComponent>(EcsWorld ecsWorld, int entity,
	                                                   EcsWorld viewEcsWorld, int viewEntity)
		where TComponent : struct
	{
		var thisPool = ecsWorld.GetPool<TComponent>();
		if (thisPool.Has(entity)) {
			ref var component = ref thisPool.Get(entity);
			var viewPool = viewEcsWorld.GetPool<TComponent>();
			ref var viewComponent = ref viewPool.Add(viewEntity);
			viewComponent = component;
		}
	}
}



}
