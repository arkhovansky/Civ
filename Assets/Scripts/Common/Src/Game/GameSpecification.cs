using System;
using System.Collections.Generic;



namespace Civ.Common.Game {



public class GameSpecification
{
	public WorldSpecification World { get; }

	public IReadOnlyList<IPlayerSpecification?> Players { get; } = new List<IPlayerSpecification?>();


	public GameSpecification()
	{
		World = new WorldSpecification();
	}
}



public class WorldSpecification
{
	public TerrainSpecification Terrain { get; }

	public List<PolitySpecification?> Polities { get; } = new();


	public WorldSpecification()
	{
		Terrain = new TerrainSpecification();
	}
}



public class TerrainSpecification
{
	public uint Width { get; set; }
	public uint Height { get; set; }
}



public interface IPlayerSpecification {}


public class HumanPlayerSpecification : IPlayerSpecification
{

}


public class AiPlayerSpecification : IPlayerSpecification
{

}



public class PolitySpecification
{
	public Guid NationKindId;
}



}
