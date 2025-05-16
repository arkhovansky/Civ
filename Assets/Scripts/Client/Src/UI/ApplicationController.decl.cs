using System.Collections.Generic;

using Civ.Client.Framework.DeclUI.Decl;



namespace Civ.Client.UI {



public class ApplicationController_Decl : IControllerDeclDefinition
{
	public IList<PropertyInitialization>? Initialization { get; }


	public ApplicationController_Decl()
	{
		var initialization = new List<PropertyInitialization>();

		var childMetadata = new Metadata("Application").Add("role", "MainMenu");
		var child = new NewControllerExpression(childMetadata);
		var childInit = new PropertyInitialization(child);
		initialization.Add(childInit);

		Initialization = initialization;
	}
}



}
