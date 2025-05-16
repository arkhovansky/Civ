namespace Civ.Common.ClientServerProtocol {



public class ObjectUpdateData
{
	public PropertyPath PropertyPath { get; }
	public object Value { get; }


	public ObjectUpdateData(PropertyPath propertyPath, object value)
	{
		PropertyPath = propertyPath;
		Value = value;
	}
}



}
