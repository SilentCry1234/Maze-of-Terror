using UnityEngine.AI;
using UnityEngine;

public class PatrolMinion : StateMinion
{

    int currentIndex = -1; //Indice del arreglo de WayPoints
    bool firstPatrol;
    public PatrolMinion(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, StateBoss _boss) : base(_npc, _agent, _anim, _player, _boss)
    {
        name = STATE.PATROL;
        agent.speed = 4.0f; //Que tan rapido se va a mover el agente
        agent.isStopped = false; //Con esto puedo detener el movimiento del agente
    }

    public override void Enter() 
    {

        Vector3 targetDir = player.position - npc.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + player.GetComponent<Player_Controller>().PlayerSpeed); //t = d/v
        float lastDis = Mathf.Infinity;
        float distance = 0;

        for (int i = 0; i < GameEnvironment.Singleton.MinionWayPoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.Singleton.MinionWayPoints[i];

            if (thisWP.gameObject != null)
            {
                distance = Vector3.Distance(player.transform.position + player.transform.forward * lookAhead * 3, thisWP.transform.position); //tal vez haya que adivinar la pos a futuro
            }

            if (distance < lastDis) //Para que continue con el ultimo punto
            {
                firstPatrol = true;
                currentIndex = i;
                lastDis = distance;
            }
        }

        anim.SetBool("Patrolling", true);
        //anim.SetTrigger("isPatrolling"); //Pone un "Evento" en el sistema que -no se usa hasta que la anim corra, si la anim no corre o 
        base.Enter();               // no necesita ser activada, el evento sigue ahi y puede causar problemas de transicion al iniciar otro trigger
    }

    public override void Update()
    {
        if (firstPatrol)
        {
            firstPatrol = false;
            if (GameEnvironment.Singleton.MinionWayPoints[currentIndex].gameObject != null)
                agent.SetDestination(GameEnvironment.Singleton.MinionWayPoints[currentIndex].transform.position);
        }
        //Si el secuaz (minion) vio al player, el boss va a correr a la ubicacion del secuaz
        else if (agent.remainingDistance < 1 && !agent.pathPending) //Si esta cerca del destino, le pongo un nuevo punto a recorrer
        {
            currentIndex = Random.Range(0, GameEnvironment.Singleton.MinionWayPoints.Count); //Selecciono un punto al azar

            if (Random.Range(0, 100) < 1) // 1% de probabldad de que vaya direct al player
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                if (GameEnvironment.Singleton.MinionWayPoints[currentIndex].gameObject != null)
                    agent.SetDestination(GameEnvironment.Singleton.MinionWayPoints[currentIndex].transform.position);
            }
        }
        if (CanAttackPlayer() && CanSeePlayer() && isAnimActive(anim, "MinionPatrol", 0))
        {
            GameEnvironment.Singleton.CallBoss(npc.transform.position);
            nextState = new AttackMinion(npc, agent, anim, player, boss);
            stage = EVENT.EXIT;
        }

        else if (CanSeePlayer() && isAnimActive(anim, "MinionPatrol", 0))
        {
            GameEnvironment.Singleton.CallBoss(npc.transform.position);
            if (AudioIA.Instance != null)
                AudioIA.Instance.PlayMinionGrowlSound();
            nextState = new PursueMinion(npc, agent, anim, player, boss);
            stage = EVENT.EXIT;
        }

        else if (IsPlayerBehind() && isAnimActive(anim, "MinionPatrol", 0))
        {
            GameEnvironment.Singleton.CallBoss(npc.transform.position);
            nextState = new PursueMinion(npc, agent, anim, player, boss);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        //anim.SetBool("Patrolling", false);
        //anim.ResetTrigger("isPatrolling");
        base.Exit();
    }

}