using System;

using Civ.Common.Game;

using Civ.Server.Framework.ReObject;



namespace Civ.Server.Domain.FrameWork {



public class GameInstance_ReObject : ObjectNode
{
	public Guid Id => ((RootNodeId)_OwnId).Id!.Value;

	public GamePhase Phase {
		get => ((Property<GamePhase>)_Properties[nameof(Phase)]).Get();
		private set => ((Property<GamePhase>)_Properties[nameof(Phase)]).Set(value);
	}

	public GameSpecification_ReObject Specification
		=> (GameSpecification_ReObject) _Nodes[nameof(Specification)];

	public World? World { get; private set; }

	public IGameMaster? GameMaster { get; private set; }



	//----------------------------------------------------------------------------------------------


	public GameInstance_ReObject()
		: base(new RootNodeId("GameInstance", Guid.NewGuid()))
	{
		_Nodes[nameof(Specification)] =
			new GameSpecification_ReObject(this, nameof(Specification));
		_Properties[nameof(Phase)] = new Property<GamePhase>(this, nameof(Phase));

		Phase = GamePhase.Setup;
	}



	public void GenerateWorld()
	{
		if (Phase != GamePhase.Setup)
			throw new InvalidOperationException();


		var worldGenerator = new WorldGenerator.WorldGenerator();
		World = worldGenerator.Create(Specification.World);

		ComputeWorldViews();

		Phase = GamePhase.Ready;
	}



	public void Start()
	{
		if (Phase != GamePhase.Ready)
			throw new InvalidOperationException();


		Phase = GamePhase.Started;

		// GameMaster = new GameMaster(this);
		// GameMaster.Start();
	}



	public IGameMaster GetGameMaster()
	{
		if (Phase != GamePhase.Started)
			throw new InvalidOperationException();

		return GameMaster!;
	}


    //----------------------------------------------------------------------------------------------
	// private


	private void ComputeWorldViews()
	{
		// VisionSystem.Run();
		// WorldViewSystem.Run();

		// foreach (var nation in World.GameSides) {
		// 	UpdateMapSystem.Run();
		// }
	}
}



}
