using System;



namespace Civ.Common.ClientServerProtocol {



public class Reply
{
	public ResultCode ResultCode { get; }

	public IResultPayload? Payload { get; }



	public Reply(ResultCode resultCode, IResultPayload? payload = null)
	{
		ResultCode = resultCode;
		Payload = payload;
	}

	public Reply(IResultPayload? payload)
		: this(ResultCode.Ok, payload)
	{}
}



public enum ResultCode
{
	Ok
}



public interface IResultPayload {}


public class CreateResult : IResultPayload
{
	public RootObjectId ObjectId { get; }


	public CreateResult(RootObjectId objectId)
	{
		ObjectId = objectId;
	}
}


public class CreateThenGetResult : IResultPayload
{
	public RootObjectId ObjectId { get; }
	public object Object { get; }


	public CreateThenGetResult(RootObjectId objectId, object @object)
	{
		ObjectId = objectId;
		Object = @object;
	}
}


public class GetResult : IResultPayload
{
	public object Object { get; }


	public GetResult(object o)
	{
		Object = o;
	}
}



}
