using UnityEngine.UIElements;

using Civ.Client.Framework.UICore.LowLevel;



namespace Civ.Client.Framework.UnityUICore.LowLevel
{
	public class UITKVisualNode : IVisualNode
	{
		public VisualElement Element { get; }



		public UITKVisualNode(VisualElement element)
		{
			Element = element;
		}
	}
}
