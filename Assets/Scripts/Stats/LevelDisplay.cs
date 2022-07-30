using System.Collections;
using System;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            text.SetText(String.Format("{0:0}", baseStats.GetLevel()));
        }
    }
}
