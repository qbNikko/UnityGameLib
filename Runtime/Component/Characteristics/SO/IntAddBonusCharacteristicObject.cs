using UnityEngine;

namespace UnityGameLib.Component.Characteristics
{
    [CreateAssetMenu(fileName = "AddIntBonus", menuName = "UnityGameLib/Characteristic/AddIntBonus", order = 1)]
    public class IntAddBonusCharacteristicObject : BonusCharacteristicObject
    {
        public int bonus;

        private ICharacteristicsBonus<int> _bonusCharacteristics;

        public override ICharacteristicsBonus<N> ToBonus<N>()
        {
            if (_bonusCharacteristics == null)
            {
                _bonusCharacteristics = new IntAddCharacteristicsBonus(bonus);
            }
            return (ICharacteristicsBonus<N>) _bonusCharacteristics;
        }
    }
}