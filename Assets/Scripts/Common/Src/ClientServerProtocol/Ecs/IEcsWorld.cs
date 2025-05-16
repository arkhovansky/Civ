namespace Civ.Common.ClientServerProtocol.Ecs {



public interface IEcsWorld
{
	IComponentPool<T> GetComponentPool<T>() where T : struct;
}



}
