using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Timer {

    public float startTime;
    public float duration;
    public float endTime { get { return startTime + duration; } }
    

    public Timer(float inDuration)
    {
        startTime = Time.time;
        duration = inDuration;
    }

    public bool isDone()
    {
        return Time.time >= startTime + duration;
    }

    public bool running()
    {
        return !isDone();
    }

    public float interpolationValue()
    {
        if (duration == 0)
            return 1;

        return (Time.time - startTime) / duration;
    }

    public static implicit operator bool(Timer t)
    {
        return !t.isDone();
    }

    public void reset()
    {
        startTime = Time.time;
    }

    public bool resetIfDone()
    {
        if (!isDone())
            return false;
        reset();
        return true;
    }

}

