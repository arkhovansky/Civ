using System;

using Unity.Transforms;
using UnityEngine;

using Civ.Common.Grid;



namespace Civ.Client.Grid {



public class VisualAbstractHexGrid
	: AbstractHexGrid
	, IVisualGrid
{
	private readonly HexCell _cell;

	private readonly float _horizontalSpacing;
	private readonly float _verticalSpacing;


	//----------------------------------------------------------------------------------------------


	public VisualAbstractHexGrid(HexOrientation orientation)
		: base(orientation)
	{
		_cell = new HexCell(orientation);

		switch (Orientation) {
			case HexOrientation.FlatTop:
				_horizontalSpacing = 0.75f * _cell.Width;
				_verticalSpacing = _cell.Height;

				break;

			case HexOrientation.PointyTop:
				_horizontalSpacing = _cell.Width;
				_verticalSpacing = 0.75f * _cell.Height;

				break;

			default:
				throw new ArgumentOutOfRangeException();
		}
	}



	public Mesh GetUnitCellMesh() => _cell.GetMesh();



	public LocalTransform GetCellLocalTransform(AxialPosition position)
	{
		float x, z;

		switch (Orientation) {
			case HexOrientation.FlatTop:
				x = position.Q * _horizontalSpacing;
				z = -0.5f * (position.Q + 2 * position.R) * _verticalSpacing;

				break;

			case HexOrientation.PointyTop:
				x = 0.5f * (2 * position.Q + position.R) * _horizontalSpacing;
				z = -(position.R * _verticalSpacing);

				break;

			default:
				throw new ArgumentOutOfRangeException();
		}

		return LocalTransform.FromPosition(x, 0, z);
	}
}



// public class VisualHexGrid
// 	: VisualAbstractHexGrid
// {
// 	private readonly float _lineOffset;
//
//
// 	//----------------------------------------------------------------------------------------------
//
//
// 	public VisualHexGrid(uint width, uint height,
// 	                     HexOrientation orientation,
// 	                     HexGridLineOffset lineOffset)
// 		: base(width, height, orientation, lineOffset)
// 	{
// 	}
//
//
//
// 	public LocalTransform GetCellLocalTransform(uint cellIndex)
// 	{
// 		uint col = cellIndex % Width;
// 		uint row = cellIndex / Width;
//
// 		float x = col * _horizontalSpacing;
// 		float z = -_cell.Height - row * _verticalSpacing;
//
// 		switch (Orientation) {
// 			case HexOrientation.FlatTop:
// 				if (LineIsOffset(col))
// 					z -= _lineOffset;
// 				break;
// 			case HexOrientation.PointyTop:
// 				if (LineIsOffset(row))
// 					x += _lineOffset;
// 				break;
//
// 			default:
// 				throw new ArgumentOutOfRangeException();
// 		}
//
// 		return LocalTransform.FromPosition(x, 0, z);
// 	}
//
//
//     //----------------------------------------------------------------------------------------------
// 	// private
//
//     private bool LineIsOffset(uint line)
//     {
// 	    return
// 		    LineOffset == HexGridLineOffset.Even && line % 2 == 0 ||
// 		    LineOffset == HexGridLineOffset.Odd && line % 2 != 0;
//     }
// }



}
