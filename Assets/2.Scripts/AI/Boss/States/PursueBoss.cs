using UnityEngine.AI;
using UnityEngine;

public class PursueBoss : StateBoss
{
    public PursueBoss(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
        agent.speed = 5; //Mayor velocidad que el estado caminar
        agent.isStopped = false; //Con esto puedo detener el movimiento del agente
    }
    public override void Enter()
    {
        anim.SetBool("isRunning", true);
        base.Enter();
    }
    public override void Update()
    {
        agent.SetDestination(player.position); //Si 
        if (agent.hasPath) //Significa que si esta siguiendo al jugador....
        { // 
            if (CanAttackPlayer())//Si esta cerca
            {
                nextState = new AttackBoss(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer()) //Si no vemos al jugador, volver a patrullar
            {
                nextState = new PatrolBoss(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
