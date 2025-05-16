using Cysharp.Threading.Tasks;



namespace Civ.Client.Framework.Reactive {



public interface IDatasetHandle {}



public interface IDatabase
{
	UniTask<IDatasetHandle> Load(string specification);
}



}
