using Civ.Common.Game;

using Civ.Server.Domain.Game;



namespace Civ.Server.Domain.FrameWork {



public interface IWorldGenerator
{
	World Create(WorldSpecification spec);
}



}
