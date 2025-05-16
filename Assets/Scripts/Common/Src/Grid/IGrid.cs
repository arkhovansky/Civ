using System.Collections.Generic;



namespace Civ.Common.Grid {



public interface IGrid
{
	uint Width { get; }
	uint Height { get; }


	OffsetPosition OffsetPositionFromCellIndex(uint cellIndex);

	// IReadOnlyList<uint> GetCellsWithinRadius(uint centerIndex, uint radius);
}



}
