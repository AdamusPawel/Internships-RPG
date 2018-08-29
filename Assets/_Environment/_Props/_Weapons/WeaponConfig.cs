using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    public class WeaponConfig : ScriptableObject
    {
        public Transform gripTransform;

        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip[] attackAnimations;
        [SerializeField] float timeBetweenAnimationCycles = .5f;
        [SerializeField] float maxAttackRange = 2f;
        [SerializeField] float minDamage = 8f;
        [SerializeField] float maxDamage = 12f;
        [SerializeField] float damageDelay = .5f;
        [SerializeField] float attackSpeed = 1.0f;

        public float GetTimeBetweenAnimationCycles()
        {
            return timeBetweenAnimationCycles;
        }

        public float GetMaxAttackRange()
        {
            return maxAttackRange;
        }

        public float GetDamageDelay()
        {
            return damageDelay;
        }

        public GameObject GetWeaponPrefab()
        {
            return weaponPrefab;
        }

        public AnimationClip GetAttackAnimClip()
        {
            RemoveAnimationEvents();
            return attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
        }

        public float GetAttackSpeed()
        {
            return 1.0f / attackSpeed; // returns attack speed per second based on weapon attack speed
        }

        public float GetMinDamage()
        {
            return minDamage;
        }

        public float GetMaxDamage()
        {
            return maxDamage;
        }

        //So that asset packs cannot cause crashes
        private void RemoveAnimationEvents()
        {
            foreach (var item in attackAnimations)
            {
                item.events = new AnimationEvent[0];
            }
        }
    }
}