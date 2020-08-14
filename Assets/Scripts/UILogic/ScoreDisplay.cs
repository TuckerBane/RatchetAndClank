using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{

    int score = 0;


    public void AddPoints(int points)
    {
        score += points;
        GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }

}
