using System;
using UnityEngine;
using UnityEngine.UI;

namespace StandardPPCalculationSystem
{
    public class PPCalculation : MonoBehaviour
    {
        public Text ppText;
        private int ppValue = 0;

        private float baseMultiplier = 1.8f;
        private float difficultyScaling = 1.4f;
        private float accuracyScaling = 1.15f;
        private float comboScaling = 1.2f;

        public int CalculatePP(float difficulty, float accuracy, int maxCombo, int achievedCombo, int missCount)
        {
            float accuracyFactor = Mathf.Pow(accuracy, accuracyScaling) * (accuracy * 100) * 0.02f;
            float comboFactor = Mathf.Pow((float)achievedCombo / maxCombo, comboScaling);
            float missPenalty = Mathf.Pow(0.95f, missCount + Mathf.Pow(missCount, 0.75f));
            float difficultyFactor = Mathf.Pow(difficulty, difficultyScaling);

            float calculatedPP = baseMultiplier * difficultyFactor * accuracyFactor * comboFactor * missPenalty;

            ppValue = Mathf.Max(0, Mathf.FloorToInt(calculatedPP));
            return ppValue;
        }

        internal double CalculatePP(double difficulty, double accuracy, int maxCombo, int achievedCombo, int missCount)
        {
            throw new NotImplementedException();
        }

        public void UpdatePPText()
        {
            ppText.text = ppValue.ToString();
        }

        void Start()
        {
            ppValue = 0;
            UpdatePPText();

            float difficulty = 6.5f;
            float accuracy = 0.965f;
            int maxCombo = 1200;
            int achievedCombo = 1100;
            int missCount = 4;

            CalculatePP(difficulty, accuracy, maxCombo, achievedCombo, missCount);
            UpdatePPText();
        }
    }
}
