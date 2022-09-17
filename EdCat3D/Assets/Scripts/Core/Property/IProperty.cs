using System;
using System.Collections.Generic;

namespace Core.Property
{
    public interface IReadOnlyProperty<T>
    {
        T Value { get; }
        event System.Action<T> OnValueChanged;
        void AddValueChangedListenter(System.Action<T> valueChangeListener);
        void RemoveValueCangedListener(System.Action<T> valueChangeListener);
    }

    public interface IProperty<T> : IReadOnlyProperty<T>
    {
        void SetValue(T newValue, bool force, bool notify = true);
    }

    public interface IIntReadOnlyProperty : IReadOnlyProperty<int>
    { }

    public interface IIntProperty : IIntReadOnlyProperty, IProperty<int>
    { }

    public class CustomProperty<T> : IProperty<T>
    {
        public T Value { get; private set; }

        public event Action<T> OnValueChanged;

        private List<System.Action<T>> _valueChangedListeners;

        public CustomProperty(T value)
        {
            Value = value;
            _valueChangedListeners = new List<Action<T>>();
        }

        public void SetValue(T newValue, bool force, bool notify = true)
        {

            if (object.Equals(Value, newValue) == false || force)
            {
                Value = newValue;

            }

            if (notify == true)
            {
                OnValueChanged?.Invoke(Value);
                NotifyListeners();
            }
        }

        public void AddValueChangedListenter(Action<T> valueChangeListener)
        {
            _valueChangedListeners.Add(valueChangeListener);
            ValidateListeners();
        }

        public void RemoveValueCangedListener(Action<T> valueChangeListener)
        {
            _valueChangedListeners.Remove(valueChangeListener);
            ValidateListeners();
        }

        private void NotifyListeners()
        {
            _valueChangedListeners.ForEach(listener => listener.Invoke(Value));
        }

        private void ValidateListeners()
        {
            var counter = 0;
            while (counter < _valueChangedListeners.Count)
            {
                var listener = _valueChangedListeners[counter];
                if (listener == null)
                {
                    _valueChangedListeners.Remove(listener);
                }
                else
                {
                    counter++;
                }
            }
        }


    }

    public interface IBoolReadOnlyProperty : IReadOnlyProperty<bool>
    { }

    public interface IBoolProperty : IBoolReadOnlyProperty, IProperty<bool>
    { }

    public class BoolProperty : CustomProperty<bool>, IBoolProperty
    {
        public BoolProperty(bool value) : base(value)
        {
        }
    }

    public class IntProperty : CustomProperty<int>, IIntProperty
    {
        public IntProperty(int value) : base(value)
        {
        }
    }

    public interface IFloatReadOnlyProperty : IReadOnlyProperty<float>
    { }

    public interface IFloatProperty : IFloatReadOnlyProperty, IProperty<float>
    { }

    public class FloatProperty : CustomProperty<float>, IFloatProperty
    {
        public FloatProperty(float value) : base(value)
        {
        }
    }
}