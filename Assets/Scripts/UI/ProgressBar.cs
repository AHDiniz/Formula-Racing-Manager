using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FormulaManager.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Value Limiters")]
        [SerializeField] private int minimum = 0;
        [SerializeField] private int maximum = 100;
        
        [Header("UI References")]
        [SerializeField] private Image mask;
        [SerializeField] private Image fill;
        [SerializeField] private Color color;

        private int current;

        public int Current { get => current; set => current = value; }

        private void Update()
        {
            mask.fillAmount = (float)(current - minimum) / (float)(maximum - minimum);

            fill.color = color;
        }
    }
}
