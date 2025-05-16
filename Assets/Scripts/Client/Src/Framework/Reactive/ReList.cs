using Civ.Common.ClientServerProtocol;



namespace Civ.Client.Framework.Reactive {



public class ReadOnlyReList
{
	protected readonly ListNode Node;


	public ReObject ReObject { get; }
	public PropertyPath PropertyPath { get; }


	public uint Count => Node.Count;



	internal ReadOnlyReList(ReObject reObject, PropertyPath path,
	                        ListNode node)
	{
		ReObject = reObject;
		PropertyPath = path;

		Node = node;
	}
}



public class ReList : ReadOnlyReList
{
	internal ReList(ReObject reObject, PropertyPath path,
	                ListNode node)
		: base(reObject, path, node)
	{}


	public void Add<T>(T value)
	{
		Node.Add<T>(value);
	}
}



}
