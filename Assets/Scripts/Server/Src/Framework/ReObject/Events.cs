namespace Civ.Server.Framework.ReObject {



public enum EventPhase
{
	Trickle,
	Bubble
}


public interface IEvent
{
	EventPhase Phase { get; set; }

	ReverseStackPath TargetPath { get; }
}



public class Event : IEvent
{
	public EventPhase Phase { get; set; }

	public ReverseStackPath TargetPath { get; } = new();
}



public class PreAddEvent : Event
{
	public PreAddEvent(IOwnId ownId)
	{
		TargetPath.Push(ownId);
	}
}



}
