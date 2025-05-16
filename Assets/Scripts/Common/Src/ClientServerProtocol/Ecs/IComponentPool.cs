namespace Civ.Common.ClientServerProtocol.Ecs {



public interface IComponentPool {}




public interface IComponentPool<TComponent> : IComponentPool
	where TComponent : struct
{
	uint Count { get; }

	ref TComponent Get(int entity);

	ref TComponent GetByIndex(uint index);

	int GetEntityByIndex(uint index);
}



}
