using System;



namespace Civ.Common.ClientServerProtocol {



public interface IObjectId {}



public struct RootObjectId : IObjectId
{
	public string Type { get; }
	public Guid? Id { get; }


	public RootObjectId(string type, Guid? id = null)
	{
		Type = type;
		Id = id;
	}
}



public struct SubObjectId : IObjectId
{
	public RootObjectId RootObjectId { get; }

	public ISubObjectId[] SubObjectPath { get; }


	public SubObjectId(RootObjectId rootObjectId, ISubObjectId[] subObjectPath)
	{
		RootObjectId = rootObjectId;
		SubObjectPath = subObjectPath;
	}
}



public interface ISubObjectId {}



public struct SubObjectName : ISubObjectId
{
	public string Name { get; }


	public SubObjectName(string name)
	{
		Name = name;
	}
}



}
