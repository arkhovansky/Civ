using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using Civ.Common.ClientServerProtocol;
using Civ.Common.ClientServerProtocol.Ecs;

using Civ.Client.Server;



namespace Civ.Client.ServerStub {



public class ServerProtocol
	: IServerProtocol
	, IEndpoint
{
	private readonly IEndpoint _serverEndpoint;

    private Dictionary<Guid, object> _requestId_To_TaskCompletionSource = new();



	public ServerProtocol(Civ.Server.Server server)
	{
		_serverEndpoint = server.ConnectClient(this);
	}



	public UniTask ExecuteRequest(IRequest request)
	{
		SendMessage(request);

		var taskCompletionSource = new UniTaskCompletionSource();
		_requestId_To_TaskCompletionSource[request.Id] = taskCompletionSource;

		return taskCompletionSource.Task;
	}


	public UniTask<TResult> ExecuteRequest<TResult>(IRequest request)
	{
		SendMessage(request);

		var taskCompletionSource = new UniTaskCompletionSource<TResult>();
		_requestId_To_TaskCompletionSource[request.Id] = taskCompletionSource;

		return taskCompletionSource.Task;
	}



	public void OnMessage(IMessage message)
	{
		if (message is ReplyEnvelope replyEnvelope)
			HandleReply(replyEnvelope.RequestId, replyEnvelope.Reply);
		else
			HandleNotification(message);
	}



	private async void SendMessage(IMessage message)
	{
		await UniTask.Yield();

		_serverEndpoint.OnMessage(message);
	}



	private void HandleReply(Guid requestId, Reply reply)
	{
		var tcsObject = _requestId_To_TaskCompletionSource[requestId];

		switch (tcsObject) {
			case UniTaskCompletionSource tcs:
				tcs.TrySetResult();
				break;

			case UniTaskCompletionSource<CreateThenGetResult> tcs:
				tcs.TrySetResult((CreateThenGetResult)reply.Payload!);
				break;

			case UniTaskCompletionSource<IEcsWorld> tcs:
				tcs.TrySetResult((IEcsWorld) ((GetResult)reply.Payload!).Object);
				break;

			default:
				throw new NotImplementedException();
		}

		_requestId_To_TaskCompletionSource.Remove(requestId);
	}


	private void HandleNotification(IMessage message)
	{

	}
}



}
