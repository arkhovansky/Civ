namespace Civ.Common.ClientServerProtocol {



public interface IEndpoint
{
	void OnMessage(IMessage message);
}



}
