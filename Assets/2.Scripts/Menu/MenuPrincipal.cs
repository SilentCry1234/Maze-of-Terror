using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public int laberynth;
    public GameObject options;


    public void StartGame()
    {
        SceneManager.LoadScene(laberynth); 
    }

    public void OptionsEntrar()
    {
        options.SetActive(true);
    }

    public void OptionsSalir()
    {
        options.SetActive(false); 
    }

    public void Quit()
    {
        Debug.Log("Sali"); 
        Application.Quit();
    }
   
}
