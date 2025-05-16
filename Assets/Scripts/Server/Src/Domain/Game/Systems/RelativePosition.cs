using Civ.Common.Grid;



namespace Civ.Server.Domain.Game.Systems {



public struct RelativePosition
{
	public AxialPosition Axial;


	public RelativePosition(AxialPosition axial)
	{
		Axial = axial;
	}
}



}
