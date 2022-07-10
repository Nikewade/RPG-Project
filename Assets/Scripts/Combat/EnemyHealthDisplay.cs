using System.Collections;
using System;
using UnityEngine;
using TMPro;

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
                text.SetText(String.Format("{0:0}%", fighter.getTarget().GetPercentage())); // format is setting the 0 index to have 0 decimal points
            }else
            {
                text.SetText("N/A");
            }
        }
    }
}