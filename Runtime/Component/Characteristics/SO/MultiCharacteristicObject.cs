using System.Collections.Generic;
using UnityEngine;

namespace UnityGameLib.Component.Characteristics
{
    [CreateAssetMenu(fileName = "MultiBonus", menuName = "UnityGameLib/Characteristic/MultiBonus", order = 1)]
    public class MultiCharacteristicObject : BonusCharacteristicObject
    {
        public Dictionary<string,BonusCharacteristicObject>  bonuses;

        private Dictionary<string, object> _convertedBonus;

        public Dictionary<string, object> ToBonus<N>()
        {
            if (_convertedBonus == null)
            {
                _convertedBonus = new Dictionary<string, object>();
                foreach (var b in bonuses)
                {
                    _convertedBonus.Add(b.Key,b.Value.ToBonus<N>());
                }
            }
            return _convertedBonus;
        }
    }
}