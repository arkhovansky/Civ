using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Civ.Common.ClientServerProtocol;
using Civ.Common.Game;

using Civ.Client.Framework.Reactive;
using Civ.Client.Framework.Reactive.ViewModels;
using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.Mvvm;

using ICommand = Civ.Client.Framework.UICore.Mvvm.ICommand;



namespace Civ.Client.UI.GameInstance.NewGameDialog {



public class NewGameDialogVM : IViewModel
{
	public GameSpecificationVM Specification { get; }

	public ICommand StartGameCommand { get; }



	public NewGameDialogVM(ReObject gameInstance, IController controller,
	                       ICommandRouter commandRouter)
	{
		Specification = new GameSpecificationVM(gameInstance);

		StartGameCommand = new Command(() => commandRouter.EmitCommand(new StartGameCommand(), controller));
	}
}



public class GameSpecificationVM : IViewModel
{
	public WorldSpecificationVM World { get; }


	public GameSpecificationVM(ReObject gameInstance)
	{
		World = new WorldSpecificationVM(gameInstance);
	}
}



public class WorldSpecificationVM : IViewModel
{
	public TerrainSpecificationVM Terrain { get; }

	public NationsVM Nations { get; }


	public WorldSpecificationVM(ReObject gameInstance)
	{
		Terrain = new TerrainSpecificationVM(gameInstance);
		Nations = new NationsVM(gameInstance);
	}
}



public class TerrainSpecificationVM : IViewModel
{
	public EditableFieldVM<uint> Width { get; }
	public EditableFieldVM<uint> Height { get; }



	public TerrainSpecificationVM(ReObject gameInstance)
	{
		Width = new EditableFieldVM<uint>(
			gameInstance, new PropertyPath(new object[] { "Specification", "World", "Terrain", "Width" }));
		Height = new EditableFieldVM<uint>(
			gameInstance, new PropertyPath(new object[] { "Specification", "World", "Terrain", "Height" }));
	}
}



public class PlayerPolityAssignment
{
	public uint Number { get; }
	public string Player { get; }
	public string Name { get; }


	public PlayerPolityAssignment(uint number, string player, string name)
	{
		Number = number;
		Player = player;
		Name = name;
	}
}



public class PlayerNationAssignment
{
	// public ValueVM<uint> Number { get; }
	public AdaptedValueVM<string, IPlayerSpecification> Player { get; }
	public SelectableVM<string, Guid> Nation { get; }


	public PlayerNationAssignment(
		ReObject gameInstance,
		int index,
		IValueAdapter<IPlayerSpecification, string> playerAdapter,
		IReadOnlyList<Guid> nationChoices,
		IValueAdapter<Guid, string> nationAdapter)
	{
		// Number = new ValueVM<uint>(
		// 	gameInstance,
		// 	new ListPropertyIndex(new PropertyPath(new[] { "Specification", "World", "Polities" }),
		// 	                      index),
		// 	numberAdapter);
		Player = new AdaptedValueVM<string, IPlayerSpecification>(
			new ReObjectProperty(gameInstance,
			                     new PropertyPath(new object[] { "Specification", "Players", index })),
			playerAdapter);
		Nation = new SelectableVM<string, Guid>(
			new ReObjectProperty(gameInstance,
			                     new PropertyPath(new object[] { "Specification", "World", "Polities", index })),
			nationChoices,
		    nationAdapter);
	}
}



internal class PlayerSpecificationVMAdapter : IValueAdapter<IPlayerSpecification, string>
{
	public string VMFromProperty(IPlayerSpecification propertyValue)
	{
		return propertyValue switch {
			HumanPlayerSpecification => "You",
			AiPlayerSpecification => "AI",

			_ => throw new ArgumentException()
		};
	}
}



internal class NationSpecificationVMAdapter : IValueAdapter<Guid, string>
{
	// private readonly IDataSet _nationChoices;
	private readonly IReadOnlyDictionary<Guid, string> _nationChoices;


	public NationSpecificationVMAdapter(IReadOnlyDictionary<Guid, string> nationChoices)
	{
		_nationChoices = nationChoices;
	}


	public string VMFromProperty(Guid propertyValue)
	{
		// return _nationChoices.For(propertyValue).Get<string>("Name");
		return _nationChoices[propertyValue];
	}
}



public class NationsVM : IViewModel
{
	// private readonly ZeroBasedIndexToNaturalNumber _numberAdapter = new();
	private readonly PlayerSpecificationVMAdapter _playerAdapter = new();


	public TableVM<PlayerNationAssignment> PlayerNationAssignments { get; }



	public NationsVM(ReObject gameInstance)
	{
		// var nationChoices = database.Select("Game.NationKind[].(Id, Name)").AsReadOnly();
		var nationChoices = new Dictionary<Guid, string> {
			[Guid.NewGuid()] = "Greeks",
			[Guid.NewGuid()] = "Romans"
		};
		var nationAdapter = new NationSpecificationVMAdapter(nationChoices);

		var list = new List<PlayerNationAssignment>();

		var polities = gameInstance.GetReadOnlyList(
			new PropertyPath(new object[] { "Specification", "World", "Polities" }));
		for (var i = 0; i < polities.Count; ++i) {
			list.Add(new PlayerNationAssignment(gameInstance, i,
			                                    _playerAdapter,
			                                    nationChoices.Keys.ToList(),
			                                    nationAdapter));
		}

		PlayerNationAssignments = new TableVM<PlayerNationAssignment>(list);
	}
}



}
