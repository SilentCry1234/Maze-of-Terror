using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    [Header("Ataque")]
    [SerializeField] int agentDamage;
    [SerializeField] BoxCollider attackCollider;
    //Boss Damage: 25
    //Minion Damage: 5

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " " + other.gameObject.name);
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador");
            other.GetComponent<Player_Health>().RestarHealth(agentDamage);
        }
    }

    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }
}