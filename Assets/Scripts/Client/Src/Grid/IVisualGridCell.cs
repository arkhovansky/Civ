using System.Collections.Generic;

using UnityEngine;



namespace Civ.Client.Grid {



public interface IVisualGridCell
{
	float Width { get; }
	float Height { get; }

	Mesh GetMesh();

	IReadOnlyList<Vector3> GetBorderVertices();

	Vector3 GetCenter();
}



}
