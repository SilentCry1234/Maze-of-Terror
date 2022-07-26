using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    [Header("Ataque")]
    [SerializeField] int agentDamage; 

    //Boss Damage: 25
    //Minion Damage: 5

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player_Health>().RestarHealth(agentDamage);
        }
    }
}