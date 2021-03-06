using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.Storage;

namespace GGJ.Spells
{
    [CreateAssetMenu(menuName = "Spells/Base Spell Stats", fileName = "SpellStats")]
    public class SpellStats : ScriptableObject
    {
        [SerializeField] ElementValue[] elements;
        [SerializeField] float range;
        [SerializeField] float damage;
        [SerializeField] float knockback;
        [SerializeField] float speed;
        [SerializeField] float cooldown;
        [SerializeField] bool piercing;

        #region properties
        public ElementValue[] Elements
        {
            get => elements;
            protected set => elements = value;
        }

        public float Range => range;
        public float Damage => damage;
        public float Knockback => knockback;
        public float Speed => speed;
        public float Cooldown => cooldown;
        public bool Piercing => piercing;
        #endregion

        /// <summary>Adds the provideds stats on top of the current stats</summary>
        /// <param name="upgrade">The stats to add to your current stats</param>
        /// <param name="switchElements">Change your current elements for the elements in the upgrade stats</param>
        public void Upgrade(SpellStats upgrade, bool switchElements = false)
        {
            if (switchElements)
                Elements = upgrade.Elements;

            range += upgrade.Range;
            damage += upgrade.Damage;
            knockback += upgrade.Knockback;
            speed += upgrade.Speed;
        }

        [System.Serializable]
        public class ElementValue
        {
            // TODO: replace BaseInventoryItem with Element
            public BaseInventoryItem element;
            public int amount;
        }
    }
}