using Civ.Common.ClientServerProtocol;

using Civ.Server.Controllers;



namespace Civ.Server.ClientProtocol {



public class ClientProtocol
	: IClientProtocol
	, IEndpoint
{
	private readonly IEndpoint _clientEndpoint;

	private readonly MainController _mainController;

	private readonly IUser _user;



	public ClientProtocol(IEndpoint clientEndpoint,
	                      MainController mainController)
	{
		_clientEndpoint = clientEndpoint;
		_mainController = mainController;
	}



	public void OnMessage(IMessage message)
	{
		var request = (Request)message;
		var userRequest = new UserRequest(_user, request);

		var reply = _mainController.HandleRequest(userRequest);

		var replyEnvelope = new ReplyEnvelope(request.Id, reply);

		SendMessage(replyEnvelope);
	}



	private void SendMessage(IMessage message)
	{
		_clientEndpoint.OnMessage(message);
	}
}



}
