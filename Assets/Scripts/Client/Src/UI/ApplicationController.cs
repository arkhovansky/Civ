using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.LowLevel.Impl;
using Civ.Client.Framework.UICore.Mvvm;
using Civ.Client.Framework.UnityUICore.HighLevel;
using Civ.Client.Server;
using Civ.Client.UI.MainMenu;



namespace Civ.Client.UI {



public class ApplicationController : ApplicationController_Base
{
	private readonly IGui _gui;
	private readonly IVvmBinder _vvmBinder;
	private readonly ICommandRouter _commandRouter;
	private readonly IServerProtocol _serverProtocol;



	public ApplicationController(IGui gui, IVvmBinder vvmBinder, ICommandRouter commandRouter, IServerProtocol serverProtocol)
		: base(commandRouter)
	{
		_gui = gui;
		_vvmBinder = vvmBinder;
		_commandRouter = commandRouter;
		_serverProtocol = serverProtocol;
	}


	public void Start()
	{
		AddChildController(
			new MainMenuController(new VisualNodePath(),
			                       _gui, _vvmBinder, _commandRouter, _serverProtocol));
	}
}



}
