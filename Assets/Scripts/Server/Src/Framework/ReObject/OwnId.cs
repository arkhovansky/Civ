using System;



namespace Civ.Server.Framework.ReObject {



public interface IOwnId {}



public struct RootNodeId : IOwnId
{
	public readonly string Type;
	public readonly Guid? Id;


	public RootNodeId(string type, Guid? id)
	{
		Type = type;
		Id = id;
	}
}



}
