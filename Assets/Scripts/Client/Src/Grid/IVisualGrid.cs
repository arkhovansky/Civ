using Unity.Transforms;
using UnityEngine;

using Civ.Common.Grid;



namespace Civ.Client.Grid {



public interface IVisualGrid
{
	Mesh GetUnitCellMesh();

	LocalTransform GetCellLocalTransform(AxialPosition position);
}



}
