using UnityEngine;

namespace UnityGameLib.Component.Characteristics
{
    public class BonusCharacteristicObject : ScriptableObject
    {
        public string description;
        public string type;

        public virtual ICharacteristicsBonus<N> ToBonus<N>()
        {
            return null;
        }
    }
}