using UnityEngine;
using UnityEngine.AI;

public class AIBoss : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] BoxCollider barrierCol;

    private NavMeshAgent agent;
    private Animator anim;
    private StateBoss currentState;
    public StateBoss BossState { get { return currentState; } set { currentState = value; } }

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
    }
    private void Start()
    { //Falta variable de control, que inicie cuando inicie el juego
        currentState = new PatrolBoss(this.gameObject, agent, anim, player); //Estado en que inicia el boss
    }

    private void Update() //Crear cond. de Si el juego esta iniciado....
    {
        currentState = currentState.Process(); //Este metodo se encarga de pasar de un estado a otro
    }
}