using Civ.Common.Grid;
using Unity.Entities;



namespace Civ.Client.Game.Ecs.Components {



public struct RelativeAxialPosition : IComponentData
{
	public AxialPosition Position;



	public RelativeAxialPosition(Civ.Common.Game.Components.RelativeAxialPosition apiRelativeAxialPosition)
	{
		Position = apiRelativeAxialPosition.Position;
	}
}



}
