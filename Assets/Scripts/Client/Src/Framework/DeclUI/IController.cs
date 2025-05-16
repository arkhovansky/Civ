using System;
using System.Collections.Generic;

using Civ.Client.Framework.UICore.LowLevel;



namespace Civ.Client.Framework.UICore.HighLevel {



public interface IController
{
	IController? Parent { get; set; }

	IReadOnlyDictionary<Type, Delegate> CommandHandlers { get; }


	void Start();

	void Destroy();


	// Delegate? TryGetCommandTypeHandler(Type commandType);


	// void AttachChildToGui(IController child);
	//
	// void DetachFromGui();
	//
	// void SetVisualNode(IVisualNode visualNode);
}



}
