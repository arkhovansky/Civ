using System;
using System.Collections.Generic;

using UnityEngine;

using Civ.Common.Grid;



namespace Civ.Client.Grid {



public class HexCell : IVisualGridCell
{
	public HexCell(HexOrientation orientation)
	{
		Orientation = orientation;

		const float size = 0.5f;

		switch (Orientation) {
			case HexOrientation.FlatTop:
				Width = 1; // 2 * size;
				Height = Mathf.Sqrt(3) * size;

				break;

			case HexOrientation.PointyTop:
				Width = Mathf.Sqrt(3) * size;
				Height = 1; // 2 * size;

				break;

			default:
				throw new ArgumentOutOfRangeException();
		}
	}


	public float Width { get; }
	public float Height { get; }

	public HexOrientation Orientation { get; }



	public Mesh GetMesh()
	{
		Mesh mesh = Orientation switch {
			HexOrientation.FlatTop =>
				new Mesh {
					vertices = new Vector3[] {
						new(0, 0, 0.5f * Height),
						new(0.25f * Width, 0, Height),
						new(0.75f * Width, 0, Height),
						new(Width, 0, 0.5f * Height),
						new(0.75f * Width, 0, 0),
						new(0.25f * Width, 0, 0),
						new(0.5f * Width, 0, 0.5f * Height),
					},
					triangles = new[] {
						0, 1, 6,
						1, 2, 6,
						2, 3, 6,
						3, 4, 6,
						4, 5, 6,
						5, 0, 6
					}
				},

			HexOrientation.PointyTop =>
				new Mesh(),

			_ => throw new ArgumentOutOfRangeException()
		};

		mesh.RecalculateNormals();

		return mesh;
	}



	public IReadOnlyList<Vector3> GetBorderVertices()
	{
		return Orientation switch {
			HexOrientation.FlatTop => new Vector3[] {
				new(0, 0, 0.5f * Height),
				new(0.25f * Width, 0, Height),
				new(0.75f * Width, 0, Height),
				new(Width, 0, 0.5f * Height),
				new(0.75f * Width, 0, 0), new(0.25f * Width, 0, 0),
				new(0.5f * Width, 0, 0.5f * Height),
			},

			HexOrientation.PointyTop => new Vector3[] {

			},

			_ => throw new ArgumentOutOfRangeException()
		};
	}


	public Vector3 GetCenter()
	{
		return new Vector3(Width / 2, 0, Height / 2);
	}
}



}
