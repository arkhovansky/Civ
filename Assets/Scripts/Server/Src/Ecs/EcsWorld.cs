using System;
using System.Collections.Generic;

using Civ.Common.ClientServerProtocol.Ecs;



namespace Civ.Server.Ecs {



public class EcsWorld : IEcsWorld
{
	private readonly Dictionary<Type, IComponentPool> _componentPools = new();



	public void AddComponentPool(IComponentPool pool)
	{
		_componentPools[pool.GetType()] = pool;
	}


	public IComponentPool<T> GetComponentPool<T>() where T : struct
	{
		return (IComponentPool<T>)_componentPools[typeof(T)];
	}
}



}
