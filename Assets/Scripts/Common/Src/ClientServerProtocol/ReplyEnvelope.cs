using System;



namespace Civ.Common.ClientServerProtocol {



public class ReplyEnvelope : IMessage
{
	public Guid RequestId { get; }

	public Reply Reply { get; }



	public ReplyEnvelope(Guid requestId, Reply reply)
	{
		RequestId = requestId;
		Reply = reply;
	}

}



}
