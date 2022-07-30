using System.Collections;
using System;
using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay: MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            if(fighter.getTarget() != null)
            {
                Health health = fighter.getTarget();
                text.SetText(String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints()));
            }
            else
            {
                text.SetText("N/A");
            }
        }
    }
}