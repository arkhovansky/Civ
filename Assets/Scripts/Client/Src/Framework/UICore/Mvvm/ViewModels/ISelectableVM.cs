using System.Collections.Generic;



namespace Civ.Client.Framework.UICore.Mvvm {



public interface ISelectableVM<T>
{
	public IReadOnlyList<T> Choices { get; }

	public VMField<uint?> Index { get; }

	VMField<bool> IsChanged { get; }

	VMField<bool> IsSynchronizing { get; }
	// VMField<bool> IsLoading { get; }
	VMField<bool> IsSaving { get; }


	void OnChange(string textValue, int index);
	void OnValueChanged(string textValue, int index);
}



}
