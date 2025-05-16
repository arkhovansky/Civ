using Cysharp.Threading.Tasks;



namespace Civ.Client.Framework.Reactive {



public interface IEcsDatasetHandle {}



public interface IEcsSynchronizer
{
	UniTask<IDatasetHandle> Load(string specification);
}



}
