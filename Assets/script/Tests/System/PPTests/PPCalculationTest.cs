using UnityEngine;

namespace StandardPPCalculationSystem

{
    public class PPCalculationTest : MonoBehaviour
    {
        void Start()
        {
            PPCalculation ppSystem = new PPCalculation();

            double difficulty = 6.5;
            double accuracy = 0.965;
            int maxCombo = 1200;
            int achievedCombo = 1100;
            int missCount = 4;

            double pp = ppSystem.CalculatePP(difficulty, accuracy, maxCombo, achievedCombo, missCount);
            Debug.Log("Performance Points: " + pp);
        }
    }
}
