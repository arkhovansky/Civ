using System.Collections.Generic;
using Civ.Client.Framework.DeclUI.Decl;



namespace Civ.Client.Framework.UICore.HighLevel.Impl {



public abstract class Controller : IController
{
	public IController? Parent { get; set; }

	// public IVisualNode? VisualNode { get; set; }


	protected readonly ControllerDeclDefinition Definition;

	protected readonly ICommandRouter CommandRouter;



	protected Controller(ControllerDeclDefinition definition,
	                     ICommandRouter commandRouter)
	{
		Definition = definition;
		CommandRouter = commandRouter;

		Parent = null;
	}



	public virtual void Start()
	{
		Initialize(Definition.Initialization);
	}

	public virtual void Destroy() {}



	protected virtual void Initialize(IReadOnlyList<PropertyInitialization> propertyInitializations)
	{
		foreach (var initialization in propertyInitializations) {
			InitProperty(initialization);
		}
	}


	protected virtual void InitProperty(PropertyInitialization initialization)
	{
		var child = Resolver.resolve<IController>(((NewControllerExpression)initialization.Initializer).Metadata);
		AddChildController(child);
	}



	protected virtual void EmitCommand(ICommand command)
	{
		CommandRouter.EmitCommand(command, this);
	}


	protected virtual void AddChildController(IController child)
	{
		child.Parent = this;
		CommandRouter.AddController(child);
	}


	protected virtual void ReplaceChildController(IController oldChild, IController newChild)
	{
		CommandRouter.RemoveController(oldChild);
		oldChild.Destroy();

		AddChildController(newChild);
	}
}



}
