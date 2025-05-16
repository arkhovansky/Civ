using System.Collections.Generic;



namespace Civ.Server.Framework.ReObject {



public abstract class Element
{
	protected Node? _Parent { get; }

	protected IOwnId? _OwnId { get; }



	protected readonly List<IRule> _Rules = new();



	public Element() {}

	public Element(Node? parent, IOwnId ownId)
	{
		_Parent = parent;
		_OwnId = ownId;
	}


	public void DispatchEvent(IEvent @event)
	{
		@event.Phase = EventPhase.Trickle;

		ProcessEvent(@event);
	}


	public void ProcessEvent(IEvent @event)
	{
		OnEvent(@event);

		if (_Parent != null) {
			@event.TargetPath.Push(_OwnId);
			_Parent.ProcessEvent(@event);
			@event.TargetPath.Pop();
		}

		@event.Phase = EventPhase.Bubble;

		// Root processes only one phase
		if (_Parent != null)
			OnEvent(@event);
	}



	protected virtual void OnEvent(IEvent @event)
	{
		foreach (var rule in _Rules) {
			rule.OnEvent(@event);
		}
	}
}



}
