using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    [Header("Ataque")]
    [SerializeField] float agentDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //RestarVida al jugador...
        }
    }


    void InfringeDamage(float playerHealth)
    {
        playerHealth =- agentDamage;
    }
}