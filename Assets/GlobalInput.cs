using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInput : MonoBehaviour
{
    public GameObject PauseScreen;

    public bool isPaused { get => Time.timeScale == 0; }


    private void Start()
    {
        if(PauseScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else if (Time.timeScale == 0)
            {
                UnpauseGame();
            }
        }

    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
        }
    }

    public void UnpauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
        }
    }
}
