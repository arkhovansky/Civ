// using System;
//
// using Civ.Common.Game;
//
// using Civ.Server.Domain.Game;
//
//
//
// namespace Civ.Server.Domain.FrameWork {
//
//
//
// public class GameInstance
// {
// 	public Guid Id { get; }
//
// 	public GamePhase Phase { get; private set; }
//
// 	public GameSpecification Specification { get; }
//
// 	public World? World { get; private set; }
//
// 	public IGameMaster? GameMaster { get; private set; }
//
//
//
// 	//----------------------------------------------------------------------------------------------
//
//
// 	public GameInstance()
// 	{
// 		Id = Guid.NewGuid();
// 		Phase = GamePhase.Setup;
// 		Specification = new GameSpecification();
// 	}
//
//
//
// 	public void GenerateWorld()
// 	{
// 		if (Phase != GamePhase.Setup)
// 			throw new InvalidOperationException();
//
//
// 		var worldGenerator = new WorldGenerator.WorldGenerator();
// 		World = worldGenerator.Create(Specification.World);
//
// 		ComputeWorldViews();
//
// 		Phase = GamePhase.Ready;
// 	}
//
//
//
// 	public void Start()
// 	{
// 		if (Phase != GamePhase.Ready)
// 			throw new InvalidOperationException();
//
//
// 		Phase = GamePhase.Started;
//
// 		GameMaster = new GameMaster(this);
// 		GameMaster.Start();
// 	}
//
//
//
// 	public IGameMaster GetGameMaster()
// 	{
// 		if (Phase != GamePhase.Started)
// 			throw new InvalidOperationException();
//
// 		return GameMaster!;
// 	}
//
//
//     //----------------------------------------------------------------------------------------------
// 	// private
//
//
// 	private void ComputeWorldViews()
// 	{
// 		// VisionSystem.Run();
// 		// WorldViewSystem.Run();
//
// 		// foreach (var nation in World.GameSides) {
// 		// 	UpdateMapSystem.Run();
// 		// }
// 	}
// }
//
//
//
// }
