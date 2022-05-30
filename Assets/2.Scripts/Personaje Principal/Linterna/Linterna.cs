using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [Header ("Luz")]
    [Space]
    [SerializeField] GameObject luz;
    public float battery;
    [SerializeField] float deductValue; // Valor 1 significa: 1 por segundo. Disminuir este valor hace que dure mas la linterna.

    [Header ("Audio")]
    [Space]
    [SerializeField] AudioSource On_Off; 
    [SerializeField] AudioSource cut;

    [Header ("Carga")]
    [Space]
    public Transform player;


    void Update()
    {
        batteryStatus();
        batteryDown();
        chargeBattery();
    }

    void batteryStatus()
    {
        if (luz.activeSelf)
        {
            //battery -= Time.deltaTime;
            battery -= deductValue * Time.deltaTime;
        }

        

        if (Input.GetKeyDown(KeyCode.F) && battery > 0)
        {
            luz.SetActive(!luz.activeSelf);
            On_Off.Play(); 
        }
    }

    void batteryDown()
    {
        if (battery <= 0f)
        {
            luz.SetActive(false);
            cut.Play();
        }

        if(battery <= 0f)
        {
            battery = 0.1f;
        }
    }

    void chargeBattery()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(player.position, player.forward, out hit, 5.5f))
            {
                if (hit.collider.gameObject.CompareTag("Battery"))
                {
                    battery += 5; 


                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
