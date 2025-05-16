using System;



namespace Civ.Common.ClientServerProtocol {



public class Request : IRequest
{
	public Guid Id { get; }

	public IObjectId ObjectId { get; }

	public string Operation { get; }

	public object? Data { get; }



	public Request(IObjectId objectId, string operation, object? data = null)
	{
		Id = Guid.NewGuid();

		ObjectId = objectId;
		Operation = operation;
		Data = data;
	}
}



}
