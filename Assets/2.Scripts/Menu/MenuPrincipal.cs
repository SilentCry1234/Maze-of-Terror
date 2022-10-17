using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public static int currentLabyrinth;
    public GameObject options;
    public GameObject credits;

    private int SelectRandomLabyrinth(int minIndex, int maxIndex)
    {
        int newIndex = Random.Range(minIndex, maxIndex + 1);
        while (newIndex == currentLabyrinth)
        {
            newIndex = Random.Range(minIndex, maxIndex + 1);

            if (newIndex != currentLabyrinth)
                return currentLabyrinth = newIndex;
        }
        return currentLabyrinth = newIndex;
    }

    public void StartGame()
    {
        SelectRandomLabyrinth(2, 4);
        SceneManager.LoadScene(currentLabyrinth);
        Time.timeScale = 1;
    }
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void OptionsEntrar()
    {
        options.SetActive(true);
    }

    public void OptionsSalir()
    {
        options.SetActive(false);
    }

    public void showCredits()
    {
        credits.SetActive(true);
    }

    public void hideCredits()
    {
        credits.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Sali");
        Application.Quit();
    }

}
