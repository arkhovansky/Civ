using System;



namespace Civ.Common.ClientServerProtocol {



public interface IRequest : IMessage
{
	Guid Id { get; }

	IObjectId ObjectId { get; }

	string Operation { get; }

	object? Data { get; }
}



}
