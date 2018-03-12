using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWithProbability /*: MonoBehaviour*/ {

  public  struct RandomSelection
    {
        private int minValue;
        private int maxValue;
        public float probability;

        public RandomSelection(int minValue, int maxValue, float probability)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.probability = probability;
        }

        public int GetValue() { return Random.Range(minValue, maxValue + 1); }
    }

    ////usage example
    //void Start()
    //{
    //    int random = GetRandomValue(
    //        new RandomSelection(0, 5, .5f),
    //        new RandomSelection(6, 8, .3f),
    //        new RandomSelection(9, 10, .2f)
    //    );
    //}

    public static int GetRandomValue(params RandomSelection[] selections)
    {
        float rand = Random.value;
        float currentProb = 0;
        foreach (var selection in selections)
        {
            currentProb += selection.probability;
            if (rand <= currentProb)
                return selection.GetValue();
        }

        //will happen if the input's probabilities sums to less than 1
        //throw error here if that's appropriate
        return -1;
    }
}
