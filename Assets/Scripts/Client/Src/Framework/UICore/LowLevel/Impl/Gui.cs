namespace Civ.Client.Framework.UICore.LowLevel.Impl {



public abstract class Gui : IGui
{
	public IVisualNode? RootVisualNode { get; private set; }


	protected IView? LastView;



	public void SetRootVisualNode(IVisualNode visualNode)
	{
		RootVisualNode = visualNode;

		LastView?.Build();
	}



	public void AddView(IView view)
	{
		//TODO

		LastView = view;
		if (RootVisualNode != null)
			view.Build();
	}

	public void RemoveView(IView view)
	{
		//TODO

		LastView = null;
	}



	public abstract IVisualNode GetVisualNode(IVisualNodePath visualNodePath);

	public abstract void SetVisualResource(IVisualNode visualNode, string resourceName);
}



}
