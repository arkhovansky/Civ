using Leopotam.EcsLite;

using Civ.Common.Grid;



namespace Civ.Server.Domain.WorldGenerator {



public interface IHexTerrainGenerator
{
	HexGrid Generate(uint width, uint height,
	                 HexOrientation orientation, HexGridLineOffset lineOffset,
	                 EcsWorld ecsWorld);
}



}
