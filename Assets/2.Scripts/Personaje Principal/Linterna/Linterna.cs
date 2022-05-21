using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [Header ("Luz")]
    [Space]
    [SerializeField] GameObject luz;
    public float battery;

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
            battery -= Time.deltaTime;
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

        if(battery <= 0)
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
