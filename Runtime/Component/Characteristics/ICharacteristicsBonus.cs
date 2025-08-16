namespace UnityGameLib.Component.Characteristics
{
    public interface ICharacteristicsBonus<T>
    {
        public T Apply(T currentValue);
    }
}