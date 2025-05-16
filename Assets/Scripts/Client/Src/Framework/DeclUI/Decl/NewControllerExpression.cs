namespace Civ.Client.Framework.DeclUI.Decl {



public class NewControllerExpression : IExpression
{
	public ComponentMetadata Metadata { get; }


	public NewControllerExpression(ComponentMetadata metadata)
	{
		Metadata = metadata;
	}
}



}
