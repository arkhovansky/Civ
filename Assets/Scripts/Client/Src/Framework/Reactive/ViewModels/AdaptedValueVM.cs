using Civ.Client.Framework.UICore.Mvvm;



namespace Civ.Client.Framework.Reactive.ViewModels {



public class AdaptedValueVM<TVM, TProperty>
	: IValueVM<TVM>
	, IPropertyObserver<TProperty>
{
	public VMField<TVM?> Value { get; }

	public VMField<bool> IsSynchronizing { get; }
	public VMField<bool> IsLoading { get; }



	private readonly ReObjectProperty _reObjectProperty;
	private readonly IValueAdapter<TProperty, TVM> _valueAdapter;



	public AdaptedValueVM(ReObjectProperty reObjectProperty, IValueAdapter<TProperty, TVM> valueAdapter)
	{
		_reObjectProperty = reObjectProperty;
		_valueAdapter = valueAdapter;

		Value = new VMField<TVM?>(
			_valueAdapter.VMFromProperty(
				_reObjectProperty.GetValue<TProperty>().Value));
		IsSynchronizing = new VMField<bool>(false);
		IsLoading = new VMField<bool>(false);

		_reObjectProperty.SubscribePropertyObserver(this);
	}


	// IPropertyObserver


	public void OnPropertyValueChanged(PropertyValue<TProperty> value)
	{
		Value.Set(_valueAdapter.VMFromProperty(value.Value));
		// IsSynchronizing.Set(false);
		// IsLoading.Set(false);
	}
}



}
