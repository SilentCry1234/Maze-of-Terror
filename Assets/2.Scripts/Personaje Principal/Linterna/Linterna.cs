using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linterna : MonoBehaviour
{
    [Header ("Luz")]
    [Space]
    [SerializeField] GameObject luz;
    public float battery;
    public Text batteryText;
    [Tooltip ("Valor 1 significa: 1 por segundo. Disminuir este valor hace que dure mas la linterna.")]
    [SerializeField] float deductValue;

    [Header ("Audio")]
    [Space]
    [SerializeField] AudioSource On_Off; 
    [SerializeField] AudioSource cut;

    [Header ("Carga")]
    [Space]
    public Transform player;


    void Update()
    {
        batteryText.text = "Battery: " + battery.ToString("0");
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
            battery = 0.01f;
        }
    }

    void chargeBattery()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(player.position, player.forward, out hit, 6.5f))
            {
                if (hit.collider.gameObject.CompareTag("Battery"))
                {
                    battery += 12.5f; 


                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    //Agregar una corrutina para disparar la linterna. Para no depender del Time.deltatime. 
}
