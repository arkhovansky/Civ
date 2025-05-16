using Cysharp.Threading.Tasks;

using Civ.Common.ClientServerProtocol;



namespace Civ.Client.Server {



public interface IServerProtocol
{
	UniTask ExecuteRequest(IRequest request);
	UniTask<TResult> ExecuteRequest<TResult>(IRequest request);
}



}
