// using Unity.Transforms;
// using UnityEngine;
//
// using Civ.Common.Grid;
//
//
//
// namespace Civ.Client.Grid {
//
//
//
// public class SquareGrid
// 	: SquareGrid
// 	, IVisualGrid
// {
// 	public SquareGrid(uint width, uint height)
// 		: base(width, height)
// 	{}
//
//
//
// 	public Mesh GetUnitCellMesh()
// 	{
// 		var mesh = new Mesh {
// 			vertices = new Vector3[] {
// 				new(0, 0, 0),
// 				new(0, 0, 1),
// 				new(1, 0, 1),
// 				new(1, 0, 0),
// 			},
// 			normals = new Vector3[] {
// 				new(0, 1, 0),
// 				new(0, 1, 0),
// 				new(0, 1, 0),
// 				new(0, 1, 0)
// 			},
// 			triangles = new[] { 0, 1, 3, 1, 2, 3 }
// 		};
//
// 		return mesh;
// 	}
//
//
// 	public LocalTransform GetCellLocalTransform(uint cellIndex)
// 	{
// 		uint col = cellIndex % Width;
// 		uint row = cellIndex / Width;
//
// 		float x = col;
// 		float z = -1 - row;
//
// 		return LocalTransform.FromPosition(x, 0, z);
// 	}
// }
//
//
//
// }
