using UnityEngine.AI;
using UnityEngine;

public class AIMinion : MonoBehaviour
{
    [SerializeField] Transform player; //Se puede buscar con una etiqueta en awake

    private NavMeshAgent agent;
    private Animator anim;
    private StateMinion currentState;
    private StateBoss boss;

    private void Awake()
    {
        boss = FindObjectOfType<AIBoss>().BossState;
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
    }
    private void Start()
    { //Falta variable de control, que inicie cuando inicie el juego

        currentState = new PatrolMinion(this.gameObject, agent, anim, player, boss); //Estado en que inicia el boss

    }

    private void Update() //Crear cond. de Si el juego esta iniciado....
    {
        currentState = currentState.Process(); //Este metodo se encarga de pasar de un estado a otro
    }
}