using System;

using Civ.Common.ClientServerProtocol.Ecs;



namespace Civ.Server.Ecs {



public class ComponentPool<T> : IComponentPool<T>
	where T : struct
{
	private T[] _denseItems;
	private int[] _sparseItems;



	public ComponentPool(T[] denseItems, int[] sparseItems)
	{
		_denseItems = denseItems;
		_sparseItems = sparseItems;
	}



	public uint Count => (uint) _denseItems.Length;


	public ref T Get(int entity)
	{
		return ref _denseItems[_sparseItems[entity]];
	}


	public ref T GetByIndex(uint index)
	{
		var internalIndex = index + 1;
		return ref _denseItems[internalIndex];
	}


	public int GetEntityByIndex(uint index)
	{
		var internalIndex = index + 1;
		return Array.IndexOf(_sparseItems, internalIndex);
	}
}



}
