using System.Collections;
using System;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience exp;

        private void Awake()
        {
            exp = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            text.SetText(String.Format("{0:0}", exp.GetExperience()));
        }
    }
}
