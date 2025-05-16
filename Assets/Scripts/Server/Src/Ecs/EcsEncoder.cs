using System;

using Civ.Common.ClientServerProtocol.Ecs;



namespace Civ.Server.Ecs {



public class EcsEncoder : IEcsEncoder
{
	public object Encode(Leopotam.EcsLite.EcsWorld ecsWorld)
	{
		var apiWorld = new Civ.Server.Ecs.EcsWorld();

		Leopotam.EcsLite.IEcsPool[]? pools = null;
		ecsWorld.GetAllPools(ref pools);

		foreach (var pool in pools) {
			var type = pool.GetType();
			var componentType = type.GetGenericArguments()[0];

			var method = type.GetMethod("GetRawDenseItems");
			var denseItems = method.Invoke(pool, null);

			method = type.GetMethod("GetRawSparseItems");
			var sparseItems = method.Invoke(pool, null);

			var apiPoolType = typeof(ComponentPool<>).MakeGenericType(componentType);
			var apiPool = (IComponentPool) Activator.CreateInstance(apiPoolType, denseItems, sparseItems);

			apiWorld.AddComponentPool(apiPool);
		}

		return apiWorld;
	}
}



}
