namespace Civ.Server.Framework.ReObject.Rules {



public enum DependencyDirection
{
	None,
	FirstOnSecond,
	SecondOnFirst
}



public class CollectionsSynchronized : IRule
{
	protected readonly IPath Path1;
	protected readonly IPath Path2;

	protected readonly DependencyDirection DependencyDirection;



	public void OnEvent(IEvent @event)
	{
		switch (@event) {
			case PreAddEvent evt:
				if (evt.Phase != EventPhase.Trickle)
					return;

				if (evt.TargetPath.Equals(Path1) && DependencyDirection == DependencyDirection.FirstOnSecond) {
					var ownId = AddItem(Path1);

				}
		}
	}
}



}
