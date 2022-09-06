using UnityEngine.AI;
using UnityEngine;

public class PursueMinion : StateMinion
{
    public PursueMinion(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, StateBoss _boss) : base(_npc, _agent, _anim, _player, _boss)
    {
        name = STATE.PURSUE;
        agent.speed = 16.0f; //Mayor velocidad que el estado caminar
        agent.isStopped = false; //Con esto puedo detener el movimiento del agente
    }

    public override void Enter()
    {
        anim.SetBool("Running", true);
        base.Enter();
    }
    public override void Update()
    {
        agent.SetDestination(player.position);
        if (agent.hasPath) //Significa que si esta siguiendo al jugador....
        { // 
            if (CanAttackPlayer())//Si esta cerca
            {
                nextState = new AttackMinion(npc, agent, anim, player, boss);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer()) //Si no vemos al jugador, volver a patrullar
            {
                Debug.Log("Termina perseguir");
                nextState = new PatrolMinion(npc, agent, anim, player, boss);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isRunning");
        anim.SetBool("Running", false);
        base.Exit();
    }
}