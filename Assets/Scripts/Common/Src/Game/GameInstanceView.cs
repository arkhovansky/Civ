using System;



namespace Civ.Common.Game {



public class GameInstanceView
{
	public Guid Id { get; set; }

	public GamePhase Phase { get; set; }

	public GameSpecification Specification { get; set; }

	public WorldView? World { get; }
}



public class WorldView
{
	// public HexGrid TerrainGrid { get; }

	public uint Turn { get; set; }
}



}
