// using Leopotam.EcsLite;
//
//
//
// namespace Civ.Server.Game {
//
//
//
// public class UpdateMapSystem : IEcsInitSystem, IEcsRunSystem
// {
// 	private readonly World _world;
//
// 	private EcsFilter _visibilityFilter;
//
// 	private EcsPool<Visibility> _visibilityPool;
//
//
// 	//----------------------------------------------------------------------------------------------
//
//
// 	public UpdateMapSystem(World world)
// 	{
// 		_world = world;
// 	}
//
//
//
// 	public void Init(IEcsSystems systems)
// 	{
// 		var ecsWorld = systems.GetWorld();
// 		_visibilityFilter = ecsWorld.Filter<Visibility>().End();
// 		_visibilityPool = ecsWorld.GetPool<Visibility>();
// 	}
//
//
// 	public void Run(IEcsSystems systems)
// 	{
// 		//TODO: Can there be entities located on absent tiles?
//
// 		foreach (var tileEntity in _tilesFilter) {
// 			ref var relativeAxialPosition = ref _axialPositionPool.Get(tileEntity);
//
// 			var relativeOffsetPosition =
// 		}
// 	}
//
//
// 	//----------------------------------------------------------------------------------------------
// 	// private
//
//
// }
//
//
//
// }
