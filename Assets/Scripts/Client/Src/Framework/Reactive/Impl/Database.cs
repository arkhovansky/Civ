// using Civ.Client.Server;
// using Civ.Common.ClientServerProtocol;
// using Cysharp.Threading.Tasks;
//
//
//
// namespace Civ.Client.Framework.Reactive.Impl {
//
//
//
// public class Database : IDatabase
// {
// 	private readonly IServerProtocol _serverProtocol;
//
//
// 	public async UniTask<IDatasetHandle> Load(string specification)
// 	{
// 		var request = new Request(new DatasetSpecification(specification), "Get");
// 		var dataset = await _serverProtocol.ExecuteRequest<Dataset>(request);
//
//
// 	}
// }
//
//
//
// }
