﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEngine.UIElements;

using Civ.Client.Framework.UICore.LowLevel;
using Civ.Client.Framework.UICore.Mvvm;
using Civ.Client.Framework.UnityUICore.LowLevel;
using Civ.Client.Framework.UnityUICore.Mvvm.Util;



namespace Civ.Client.Framework.UnityUICore.Mvvm {



public class VvmBinder : IVvmBinder
{
	public void Bind(IVisualNode visualNode, object viewModel)
	{
		var visualElement = (visualNode as UITKVisualNode)!.Element;
		(new Binder(visualElement, viewModel)).Bind();
	}



	private class Binder
	{
		private readonly VisualElement _rootElement;
		private readonly object _viewModel;



		public Binder(VisualElement rootElement, object viewModel)
		{
			_rootElement = rootElement;
			_viewModel = viewModel;
		}


		public void Bind()
		{
			var type = _viewModel.GetType();
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			foreach (var property in properties) {
				if (property.PropertyType == typeof(string)) {
					var matchingElement = FindMatchingElement<TextElement>(property.Name);

					if (matchingElement != null)
						BindStatic(matchingElement, (string)property.GetValue(_viewModel));
					// else
				}
				else if (typeof(ICommand).IsAssignableFrom(property.PropertyType)) {
					string nameBase = property.Name.TrimSuffix("Command");
					var names = new [] { nameBase, nameBase + "Button", nameBase + "Command" };

					var matchingElement = FindMatchingElement<Button>(names);

					if (matchingElement != null) {
						matchingElement.BindCommand((ICommand)property.GetValue(_viewModel));
					}
					// else
				}
				else if (typeof(IEditableFieldVM<>).IsAssignableFrom(property.PropertyType)) {

				}
				else if (property.PropertyType.Name == "EditableFieldVM`1") {
					Bind<TextField>(property,
					                "Field", new[] { "Field" },
					                typeof(TextFieldExtensions));
				}
				else if (property.PropertyType.Name == "TableVM`1") {
					Bind<MultiColumnListView>(property,
					                          "Table", new[] { "Table", "List" },
					                          typeof(MultiColumnListViewExtensions));
				}
				else if (property.PropertyType.IsClass) {
					var elementNames = GetVisualElementNames(property.Name,
					                                         "VM", new [] { "Block", "Pane" });
					var matchingElement = FindMatchingElementStrict<VisualElement>(elementNames);

					if (matchingElement != null) {
						var value = property.GetValue(_viewModel);
						if (value != null)
							(new Binder(matchingElement, value)).Bind();
						else
							matchingElement.style.display = DisplayStyle.None;
					}
					else {
					}
				}
			}
		}



		private void Bind<TVisualElement>(PropertyInfo property,
		                                  string propertySuffix, string[] visualElementSuffixes,
		                                  Type visualElementExtensionsType)
			where TVisualElement : VisualElement
		{
			var visualElementNames = GetVisualElementNames(property.Name, propertySuffix, visualElementSuffixes);

			var matchingElement = FindMatchingElement<TVisualElement>(visualElementNames);

			if (matchingElement != null) {
				var controlVM = property.GetValue(_viewModel);
				var controlVMType = controlVM.GetType();
				var bindViewModel_Generic = visualElementExtensionsType.GetMethod("BindViewModel");
				var bindViewModel = bindViewModel_Generic!.MakeGenericMethod(controlVMType.GetGenericArguments());
				bindViewModel.Invoke(null, new object[] { matchingElement, controlVM });
			}
			else
				Debug.LogError($"VvmBinder: Matching visual element not found for property '{property.Name}'");
		}


		private static string[] GetVisualElementNames(string propertyName,
		                                       string propertySuffix, string[] visualElementSuffixes)
		{
			var nameBase = propertyName.TrimSuffix(propertySuffix);

			return Enumerable.Concat(new[] { nameBase },
			                         visualElementSuffixes.Select(it => nameBase + it)).ToArray();
		}



		private T? FindMatchingElement<T>(string propertyName)
			where T : VisualElement
		{
			return FindMatchingElement<T>(_rootElement,
			                              new string[]{propertyName},
			                              it => it is T);
		}

		private T? FindMatchingElementStrict<T>(string propertyName)
			where T : VisualElement
		{
			return FindMatchingElement<T>(_rootElement,
			                              new string[]{propertyName},
			                              it => it.GetType() == typeof(T));
		}

		private T? FindMatchingElement<T>(IReadOnlyList<string> names)
			where T : VisualElement
		{
			return FindMatchingElement<T>(_rootElement, names,
			                              it => it is T);
		}

		private T? FindMatchingElementStrict<T>(IReadOnlyList<string> names)
			where T : VisualElement
		{
			return FindMatchingElement<T>(_rootElement, names,
			                              it => it.GetType() == typeof(T));
		}


		private static T? FindMatchingElement<T>(VisualElement element,
		                                         IReadOnlyList<string> names,
		                                         Predicate<VisualElement> filter)
			where T : VisualElement
		{
			if (filter(element) && NamesMatch(element.name, names))
				return (T) element;

			foreach (var child in element.Children()) {
				var matchingElement = FindMatchingElement<T>(child, names, filter);

				if (matchingElement != null)
					return matchingElement;
			}

			return null;
		}



		private static bool NamesMatch(string elementName, IReadOnlyList<string> names)
		{
			return names.Any(name => NamesMatch(elementName, name));
		}


		private static bool NamesMatch(string elementName, string name)
		{
			return elementName.Equals(name, StringComparison.OrdinalIgnoreCase);
		}



		private static void BindStatic(TextElement element, string text)
		{
			element.text = text;
		}
	}
}



}
