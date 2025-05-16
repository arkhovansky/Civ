using System;



namespace Civ.Client.Framework.UICore.Mvvm {



public class VMField<T>
{
	public T Value { get; private set; }


	private Action<T>? _action;



	public VMField(T value)
	{
		Value = value;
	}


	public void Set(T value)
	{
		SetWithoutNotification(value);

		_action?.Invoke(value);
	}

	public void SetWithoutNotification(T value)
	{
		if ((value == null && Value == null) || value!.Equals(Value))
			return;

		Value = value;
	}


	public void Listen(Action<T> action)
	{
		_action = action;
	}
}



}
