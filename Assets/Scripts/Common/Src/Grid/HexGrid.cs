using System;
using System.Collections.Generic;



namespace Civ.Common.Grid {



public enum HexGridLineOffset
{
	Even = 1,  // 0, 2, ...
	Odd = -1
}



public struct AxialPosition
{
	public int Q;
	public int R;


	public AxialPosition(int q, int r)
	{
		Q = q;
		R = r;
	}


	public static AxialPosition operator +(AxialPosition a, AxialPosition b)
		=> new AxialPosition(a.Q + b.Q, a.R + b.R);

	public static AxialPosition operator -(AxialPosition a, AxialPosition b)
		=> new AxialPosition(a.Q - b.Q, a.R - b.R);
}



public class AbstractHexGrid
{
	public HexOrientation Orientation { get; }



	public AbstractHexGrid(HexOrientation orientation)
	{
		Orientation = orientation;
	}



	public IReadOnlyList<AxialPosition> GetCellsWithinRadius(AxialPosition center, uint radius)
	{
        // r c          n  total
		// 0 1          1
        // 1 2          6   7
        // 2 3  6+2*3 =12  19
        // 3 4  8+2*5 =18  37

        // 1 + 6 * r*(r+1)/2

        var R = (int)radius;

        var cellCount = 1 + 6 * (R*(R+1)/2);
        var cellPositions = new AxialPosition[cellCount];

        var i = 0;
        for (var q = -R; q <= R; ++q)
	        for (var r = Math.Max(-R, -q-R); r <= Math.Min(R, -q+R); ++r)
		        cellPositions[i++] = center + new AxialPosition(q, r);

        return cellPositions;
	}
}



public class HexGrid
	: AbstractHexGrid
	, IGrid
{
	public uint Width { get; }
	public uint Height { get; }

	public HexGridLineOffset LineOffset { get; }



	public HexGrid(uint width, uint height,
	               HexOrientation orientation,
	               HexGridLineOffset lineOffset)
		: base(orientation)
	{
		Width = width;
		Height = height;
		LineOffset = lineOffset;
	}



	public OffsetPosition OffsetPositionFromCellIndex(uint cellIndex)
		=> new(cellIndex % Width, cellIndex / Width);


	public uint CellIndexFrom(OffsetPosition offsetPosition)
		=> offsetPosition.Row * Height + offsetPosition.Col;

	public uint CellIndexFrom(AxialPosition axial)
		=> CellIndexFrom(OffsetPositionFrom(axial));


	public OffsetPosition OffsetPositionFrom(AxialPosition axial)
	{
		int col, row;

		switch (Orientation) {
			case HexOrientation.FlatTop:
				col = axial.Q;
				row = axial.R + (axial.Q + (int)LineOffset * (axial.Q & 1)) / 2;

				break;

			case HexOrientation.PointyTop:
				col = axial.Q + (axial.R + (int)LineOffset * (axial.R & 1)) / 2;
				row = axial.R;

				break;

			default:
				throw new ArgumentOutOfRangeException();
		}

		if (col < 0 || row < 0)
			throw new ArgumentOutOfRangeException();

		return new OffsetPosition((uint)col, (uint)row);
	}


	public AxialPosition AxialPositionFromCellIndex(uint cellIndex)
		=> AxialPositionFrom(OffsetPositionFromCellIndex(cellIndex));


	public AxialPosition AxialPositionFrom(OffsetPosition offsetPosition)
	{
		int col = (int)offsetPosition.Col;
		int row = (int)offsetPosition.Row;

		int q, r;

		switch (Orientation) {
			case HexOrientation.FlatTop:
				q = col;
				r = row - (col + (int)LineOffset * (col & 1)) / 2;
				return new AxialPosition(q, r);

			case HexOrientation.PointyTop:
				q = col - (row + (int)LineOffset * (row & 1)) / 2;
				r = row;
				return new AxialPosition(q, r);

			default:
				throw new ArgumentOutOfRangeException();
		}
	}



	// public IReadOnlyList<uint> GetCellsWithinRadius(uint centerIndex, uint radius)
	// {
 //        // r c          n  total
	// 	// 0 1          1
 //        // 1 2          6   7
 //        // 2 3  6+2*3 =12  19
 //        // 3 4  8+2*5 =18  37
 //
 //        // 1 + 6 * r*(r+1)/2
 //
 //        var R = (int)radius;
 //
 //        var cellCount = 1 + 6 * (R*(R+1)/2);
 //        var cellIndices = new uint[cellCount];
 //
 //        var centerAxial = AxialPositionFromCellIndex(centerIndex);
 //
 //        var i = 0;
 //        for (var q = -R; q <= R; ++q)
	//         for (var r = Math.Max(-R, -q-R); r <= Math.Min(R, -q+R); ++r)
	// 	        cellIndices[i++] = CellIndexFrom(centerAxial + new AxialPosition(q, r));
 //
 //        return cellIndices;
	// }
}



}
