using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ValueRange
{
    public float min, max;
    public float value { get { return Random.Range(min, max); } }
}

static class RandomExtensions
{
    public static void Shuffle<T>(T[] array)
    {
        if(array.Length == 0)
            return;

        int n = array.Length - 1;
        while (n > 1)
        {
            int k = Random.Range(0,n);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
            --n;
        }
    }
}
