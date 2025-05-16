using System;
using System.Collections.Generic;

using Civ.Common.ClientServerProtocol;
using Civ.Common.Game;

using Civ.Client.Framework.Reactive;
using Civ.Client.Server;



namespace Civ.Client.Game {



public class GameInstanceViewReObject : ReObject
{
	public GameInstanceViewReObject(Guid id, object gameInstance,
	                                IServerProtocol serverProtocol)
		: base(new RootObjectId("GameInstance", id), CreateChildObjects(), CreateProperties(),
		       serverProtocol)
	{
		var gameInstance_ = (GameInstanceView)gameInstance;

		SetPropertyValue(new PropertyPath(new object[] { "Phase" }), gameInstance_.Phase);

		SetPropertyValue(new PropertyPath(new object[] { "Specification", "World", "Terrain", "Width" }),
		                 gameInstance_.Specification.World.Terrain.Width);
		SetPropertyValue(new PropertyPath(new object[] { "Specification", "World", "Terrain", "Height" }),
		                 gameInstance_.Specification.World.Terrain.Height);

		GetList(new PropertyPath(new object[] { "Specification", "World", "Polities" }))
			.Add(gameInstance_.Specification.World.Polities[0]);
		GetList(new PropertyPath(new object[] { "Specification", "World", "Polities" }))
			.Add(gameInstance_.Specification.World.Polities[1]);
		GetList(new PropertyPath(new object[] { "Specification", "Players" }))
			.Add<IPlayerSpecification?>(/*gameInstance_.Specification.Players[0]*/ new HumanPlayerSpecification());
		GetList(new PropertyPath(new object[] { "Specification", "Players" }))
			.Add<IPlayerSpecification?>(/*gameInstance_.Specification.Players[1]*/ new AiPlayerSpecification());
	}



	private static Dictionary<string, INode> CreateChildObjects()
	{
		var terrainProperties = new Dictionary<string, IProperty> {
			["Width"] = new Property<uint>(),
			["Height"] = new Property<uint>()
		};
		var terrain = new ObjectBase(terrainProperties);

		var worldObjects = new Dictionary<string, INode> {
			["Terrain"] = terrain,
			["Polities"] = new ListNode()
		};
		var world = new ObjectBase(worldObjects);

		var specificationObjects = new Dictionary<string, INode> {
			["World"] = world,
			["Players"] = new ListNode()
		};
		var specification = new ObjectBase(specificationObjects);

		var childObjects = new Dictionary<string, INode> {
			["Specification"] = specification
		};

		return childObjects;
	}


	private static Dictionary<string, IProperty> CreateProperties()
	{
		return new Dictionary<string, IProperty> {
			["Phase"] = new Property<GamePhase>()
		};
	}
}



}
