using UnityEngine;
using UnityEngine.UIElements;

using Civ.Server;

using Civ.Client.Framework.UICore.HighLevel;
using Civ.Client.Framework.UICore.HighLevel.Impl;
using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;
using Civ.Client.Framework.UnityUICore.LowLevel;
using Civ.Client.Framework.UnityUICore.Mvvm;
using Civ.Client.Server;
using Civ.Client.ServerStub;
using Civ.Client.UI;



public class Application : MonoBehaviour
{
	private IGui _gui;
	private IVvmBinder _vvmBinder;
	private ICommandRouter _commandRouter;
	private IServerProtocol _serverProtocol;



	private void Awake()
	{
		_gui = new Gui();
		_vvmBinder = new VvmBinder();
		_commandRouter = new CommandRouter();

		var server = new Server();
		_serverProtocol = new ServerProtocol(server);
	}



	private void Start()
	{
		var applicationController = new ApplicationController(_gui, _vvmBinder, _commandRouter, _serverProtocol);
		// _commandRouter.SetRootController(applicationController);
		applicationController.Start();
	}



	private void OnEnable()
	{
		// OnEnable is called after Live Reload when UIDocument's UXML asset is reloaded

		var uiDocument = GetComponent<UIDocument>();

		_gui.SetRootVisualNode(new UITKVisualNode(uiDocument.rootVisualElement));
	}


	private void Update()
	{
		_commandRouter.Update();
	}
}
