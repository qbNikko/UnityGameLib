using System;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Misc;

namespace UnityGameLib.Component.Characteristics
{
    public class MultiCharacteristics<T> : ICharacteristics<T>
    {
        public event Action<T> OnChange;
        protected List<ICharacteristicsBonus<T>> _characteristicsBonusList;
        protected T _startValue;
        protected T _currentValue;

        public T CurrentValue => _currentValue;

        public MultiCharacteristics(T startValue, T currentValue)
        {
            _characteristicsBonusList = new List<ICharacteristicsBonus<T>>();
            _startValue = startValue;
            _currentValue = startValue;
        }

        public void Add(ICharacteristicsBonus<T> characteristics)
        {
            _characteristicsBonusList.Add(characteristics);
            _currentValue = Calculate();
            OnChange?.Invoke(_currentValue);
        }

        public void Remove(ICharacteristicsBonus<T> characteristics)
        {
            _characteristicsBonusList.Remove(characteristics);
            _currentValue = Calculate();
            OnChange?.Invoke(_currentValue);
        }

        public bool Check(ICharacteristicsBonus<T> characteristics)
        {
            return _characteristicsBonusList.Contains(characteristics);
        }

        public T Calculate()
        {
            T value = _startValue;
            foreach (ICharacteristicsBonus<T> bonus in _characteristicsBonusList)
            {
                value = bonus.Apply(value);
            }
            return value;
        }
    }
}