using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;
using RPG.Saving;
using RPG.Core;
using GameDevTV.Utils;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        LazyValue<float> healthPoints; // LazyValue allows to make of a Delegate or "Event" to tell something to be intialize or set when it is called
        // so health points, whenever it is used anywhere, it will be set so it is not null. It is forced to be init in start

        bool isDead = false;

        private void Awake() {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable() {
            GetComponent<BaseStats>().onLevelUp += RestoreHealthOnLevelUp;
        }

        private void OnDisable() {
            GetComponent<BaseStats>().onLevelUp -= RestoreHealthOnLevelUp;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if(healthPoints.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public void RestoreHealthOnLevelUp()
        {
            healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float) state;

            if (healthPoints.value == 0)
            {
                Die();
            }
        }
    }
}
