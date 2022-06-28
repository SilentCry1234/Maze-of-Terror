using UnityEngine.AI;
using UnityEngine;

public class PatrolBoss : StateBoss
{
    int currentIndex = 0; //Indice del arreglo de WayPoints
    bool firstPatrol;
    public PatrolBoss(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        agent.speed = 14; //Que tan rapido se va a mover el agente
        agent.isStopped = false; //Con esto puedo detener el movimiento del agente
    }

    public override void Enter() //El singleton Environment guarda las posiciones de los WayPoints
    {

        Vector3 targetDir = player.position - npc.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + player.GetComponent<Player_Controller>().PlayerSpeed); //t = d/v
        float lastDis = Mathf.Infinity;
        float distance = 0;
        for (int i = 0; i < GameEnvironment.Singleton.BossWayPoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.Singleton.BossWayPoints[i];

            if (thisWP.gameObject != null)
                distance = Vector3.Distance(player.transform.position + player.transform.forward * lookAhead * 3, thisWP.transform.position); //tal vez haya que adivinar la pos a futuro

            if (distance < lastDis) //Para que continue con el ultimo punto
            {
                firstPatrol = true;
                currentIndex = i;
                lastDis = distance;
            }
        }
        anim.SetBool("isPatrolling", true); //Pone un "Evento" en el sistema que -no se usa hasta que la anim corra, si la anim no corre o 
        base.Enter();               // no necesita ser activada, el evento sigue ahi y puede causar problemas de transicion al iniciar otro trigger
    }

    public override void Update()
    {
        if (firstPatrol)
        {
            firstPatrol = false;
            if (GameEnvironment.Singleton.BossWayPoints[currentIndex] != null)
                agent.SetDestination(GameEnvironment.Singleton.BossWayPoints[currentIndex].transform.position);
        }
        //Si el secuaz (minion) vio al player, el boss va a correr a la ubicacion del secuaz
        else if (agent.remainingDistance < 1) //Si esta cerca del destino, le pongo un nuevo punto a recorrer
        {
            currentIndex = Random.Range(0, GameEnvironment.Singleton.BossWayPoints.Count); //Selecciono un punto al azar

            if (Random.Range(0, 100) < 1) // 1% de probabldad de que vaya direct al player
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                if (GameEnvironment.Singleton.MinionWayPoints != null && GameEnvironment.Singleton.BossWayPoints[currentIndex] != null)
                    agent.SetDestination(GameEnvironment.Singleton.BossWayPoints[currentIndex].transform.position);
            }
        }
        if (CanAttackPlayer() && CanSeePlayer() && isAnimActive(anim, "BossPatrol", 0))
        {
            nextState = new AttackBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

        else if (CanSeePlayer() && isAnimActive(anim, "BossPatrol", 0))
        {
            nextState = new PursueBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

        else if (IsPlayerBehind() && isAnimActive(anim, "BossPatrol", 0))
        {
            nextState = new PursueBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

        else if (MinionCalling())
        {
            Debug.Log("MinionLlamando");
            nextState = new RunBoss(npc, agent, anim, player, minionPos);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        //anim.ResetTrigger("isPatrolling");
        base.Exit();
    }
}
