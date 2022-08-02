using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pausa;
    bool isPaused;

    private void Awake()
    {
        Time.timeScale = 1;
        pausa.SetActive(false); 
        isPaused = false;
    }

    void Update()
    {
        Pausa(); 
    }

    private void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;
            pausa.SetActive(true);
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            pausa.SetActive(false);
            isPaused = false;
        }
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene(0); 
    }
}
