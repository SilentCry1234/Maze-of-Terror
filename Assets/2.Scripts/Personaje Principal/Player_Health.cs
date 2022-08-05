using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [Header ("HEALTH")]
    [Space]
    public float health = 500f;
    public Text healthText;
    public GameObject gameOver;

    [Header ("Stun")]
    [Space]
    public float stunTime = 2f;

    private void Awake()
    {
        gameOver.SetActive(false);
    }

    private void Update()
    {
        healthText.text = "HEALTH: " + health.ToString("0");
    }

    public void RestarHealth(int cantidad)
    {
        if(health > 0)
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.player_Hit); 
            health -= cantidad; 
            StartCoroutine(StunPlayer()); 

            if(health <= 0)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.playerDeath);
                GameOver(); 
            }
        }
    }


    void GameOver()
    {
        Debug.Log("Me mori xD");
        Time.timeScale = 0; 
        gameOver.SetActive(true);
    }


    IEnumerator StunPlayer()
    {
        var velocidadNormal = GetComponent<Player_Controller>().playerSpeed;
        var velocidadRun = GetComponent<Player_Controller>().runSpeed;

        GetComponent<Player_Controller>().playerSpeed = 0;
        GetComponent<Player_Controller>().runSpeed = 0;

        yield return new WaitForSeconds(stunTime);

        GetComponent<Player_Controller>().playerSpeed = 12f;
        GetComponent<Player_Controller>().runSpeed = 30f;
    }
}
