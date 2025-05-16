namespace Civ.Client.Framework.UICore.Mvvm {



public interface IValueVM<T>
{
	VMField<T?> Value { get; }

	VMField<bool> IsSynchronizing { get; }
	VMField<bool> IsLoading { get; }
}



}
