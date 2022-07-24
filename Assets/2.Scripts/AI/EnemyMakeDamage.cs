using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMakeDamage : MonoBehaviour
{
    public int cantidad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player_Health>().RestarHealth(cantidad);
        }
    }

}
