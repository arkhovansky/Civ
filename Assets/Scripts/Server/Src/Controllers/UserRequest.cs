using Civ.Common.ClientServerProtocol;



namespace Civ.Server.Controllers {



public class UserRequest
{
	public IUser User { get; }

	public Request Request { get; }



	public UserRequest(IUser user, Request request)
	{
		User = user;
		Request = request;
	}
}



}
