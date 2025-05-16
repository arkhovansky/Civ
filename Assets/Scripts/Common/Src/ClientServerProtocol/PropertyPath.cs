namespace Civ.Common.ClientServerProtocol {



public struct PropertyPath
{
	public object[] Segments { get; }


	public PropertyPath(object[] segments)
	{
		Segments = segments;
	}
}



}
