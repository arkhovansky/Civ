namespace Civ.Client.Framework.UICore.Mvvm {



public interface IEditableFieldVM<T>
	where T : struct
{
	VMField<T?> Value { get; }

	VMField<bool> IsChanged { get; }

	VMField<bool> IsSynchronizing { get; }
	// VMField<bool> IsLoading { get; }
	VMField<bool> IsSaving { get; }


	void OnLostFocusChange(string textValue);

	void OnChange(string textValue);
}



}
