using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameLib.Collection;

namespace UnityGameLib.Component.Characteristics
{
    [Flags]
    public enum AAA
    {
        A=0,B=1,C=2,D=4,E=8,F=16
    }
    [CreateAssetMenu(fileName = "MultiBonus", menuName = "UnityGameLib/Characteristic/MultiBonus", order = 1)]
    public class MultiCharacteristicObject : BonusCharacteristicObject
    {
        public SerializableDictionary<string,BonusCharacteristicObject>  bonuses;
        public SerializableDictionary<AAA,BonusCharacteristicObject>  bonuses2;

        private Dictionary<string, object> _convertedBonus;

        public new Dictionary<string, object> ToBonus<N>()
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