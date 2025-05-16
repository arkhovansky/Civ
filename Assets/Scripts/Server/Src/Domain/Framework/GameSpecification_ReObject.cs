using System;
using System.Linq;

using Civ.Server.Framework.ReObject;



namespace Civ.Server.Domain.FrameWork {



public class GameSpecification_ReObject : ObjectNode
{
	public WorldSpecification_ReObject World
		=> (WorldSpecification_ReObject) _Nodes[nameof(World)];

	public ListNode<GameSideSpecification_ReObject> GameSides
		=> (ListNode<GameSideSpecification_ReObject>) _Nodes[nameof(GameSides)];



	private readonly _Rule _gameSidesAndPolitiesSync;



	public GameSpecification_ReObject(Node parent, string ownId)
		: base(parent, ownId)
	{
		_Nodes[nameof(World)] =
			new WorldSpecification_ReObject(this, nameof(World));
		_Nodes[nameof(GameSides)] =
			new ListNode<GameSideSpecification_ReObject>(this, nameof(GameSides));
	}



}




internal class GameSidesAndPolitiesInSyncRule : IModifyingRule
{
	void Run(GameSpecification_ReObject gameSpecification, IChangeSet changeSet)
	{
		var politiesChanges = changeSet.Where(it => it.Path.RelativeTo(gameSpecification).Equals("World.Polities")).ToList();

		var polityAdditions = politiesChanges.Where(it => it.Operation is ListOperation.Add);
		foreach (var polityAddition in polityAdditions) {
			var polityReference = new ObjectNodeReference(polityAddition.Path + polityAddition.Key);
			var gameSide = new GameSideSpecification_ReObject(polityReference);
			gameSpecification.GameSides.Add(gameSide);
		}

		var polityDeletions = politiesChanges.Where(it => it.Operation is ListOperation.Delete);
		foreach (var polityDeletion in polityDeletions) {
			var gameSide = gameSpecification.GameSides.First(it => it.Polity.Equals(polityDeletion.ItemPath));
			gameSpecification.GameSides.Delete(gameSide);
		}

		var gameSidesChanges = changeSet.Where(it => it.Path.RelativeTo(gameSpecification).Equals("GameSides")).ToList();

		var gameSideAdditions = gameSidesChanges.Where(it => it.Operation is ListOperation.Add);
		foreach (var gameSideAddition in gameSidesAdditions) {
			var (polity, key) = gameSpecification.World.Polities.AddDefault();
			var polityLink = new Link(gameSpecification._Path + key);
			var gameSide = (GameSideSpecification_ReObject) gameSideAddition.Item;
			gameSide.Polity = polityLink;
		}

		var gameSideDeletions = gameSidesChanges.Where(it => it.Operation is ListOperation.Delete);
		foreach (var gameSideDeletion in gameSidesDeletions) {
			var gameSide = (GameSideSpecification_ReObject) gameSideDeletion.Item;
			gameSpecification.World.Polities.Delete(gameSide.Polity.Key);
		}
	}
}



public class WorldSpecification_ReObject : ObjectNode
{
	public TerrainSpecification_ReObject Terrain
		=> (TerrainSpecification_ReObject) _Nodes[nameof(Terrain)];

	public ListNode<PolitySpecification_ReObject> Polities
		=> (ListNode<PolitySpecification_ReObject>) _Nodes[nameof(Polities)];



	public WorldSpecification_ReObject(Node parent, string ownId)
		: base(parent, ownId)
	{
		_Nodes[nameof(Terrain)] =
			new TerrainSpecification_ReObject(this, nameof(Terrain));
		_Nodes[nameof(Polities)] =
			new ListNode<PolitySpecification_ReObject>(this, nameof(Polities));
	}
}



public class TerrainSpecification_ReObject : ObjectNode
{
	public uint? Width {
		get => ((Property<uint?>) _Properties[nameof(Width)]).Get();
		set => ((Property<uint?>) _Properties[nameof(Width)]).Set(value);
	}
	public uint? Height {
		get => ((Property<uint?>) _Properties[nameof(Height)]).Get();
		set => ((Property<uint?>) _Properties[nameof(Height)]).Set(value);
	}



	public TerrainSpecification_ReObject(Node parent, string ownId)
		: base(parent, ownId)
	{
		_Properties[nameof(Width)] = new Property<uint?>(this, nameof(Width));
		_Properties[nameof(Height)] = new Property<uint?>(this, nameof(Height));
	}
}



public class PolitySpecification_ReObject : ObjectNode
{
	public Guid? NationKind {
		get => ((Property<Guid?>) _Properties[nameof(NationKind)]).Get();
		set => ((Property<Guid?>) _Properties[nameof(NationKind)]).Set(value);
	}



	public PolitySpecification_ReObject()
	{
		Construct();
	}


	private void Construct()
	{
		_Properties[nameof(NationKind)] = new Property<Guid?>(this, nameof(NationKind));
	}
}



public class GameSideSpecification_ReObject : ObjectNode
{
	public PolitySpecification_ReObject Polity
		=> (PolitySpecification_ReObject) _Nodes[nameof(Polity)];



	public GameSideSpecification_ReObject(IPath referencePath)
	{
		Construct(referencePath);
	}


	private void Construct(IPath referencePath)
	{
		_Nodes[nameof(Polity)] = new ObjectNodeReference(this, nameof(Polity),
		                                                 referencePath);
	}
}



}
