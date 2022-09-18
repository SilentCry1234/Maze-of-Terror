using UnityEngine.AI;
using UnityEngine;

public class AIMinion : MonoBehaviour
{
    [SerializeField] Transform player; //Se puede buscar con una etiqueta en awake


    private GameManager gameManager;
    private NavMeshAgent agent;
    private Animator anim;
    private StateMinion currentState;
    private StateBoss boss;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        boss = FindObjectOfType<AIBoss>().BossState;
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
    }
    private void Start()
    { 
        currentState = new PatrolMinion(this.gameObject, agent, anim, player, boss); //Estado en que inicia el boss
    }

    private void Update() //Crear cond. de Si el juego esta iniciado....
    {
        if(gameManager.IsGameStarted)
           currentState = currentState.Process(); //Este metodo se encarga de pasar de un estado a otro
    }
}