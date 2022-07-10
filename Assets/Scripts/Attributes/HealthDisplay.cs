using System.Collections;
using System;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            text.SetText(String.Format("{0:0}%", health.GetPercentage())); // format is setting the 0 index to have 0 decimal points
        }
    }
}