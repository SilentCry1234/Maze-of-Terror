using UnityEngine.AI;
using UnityEngine;

public class RunBoss : StateBoss
{
    public RunBoss(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Vector3 _minionPos) : base(_npc, _agent, _anim, _player, _minionPos)
    {
        name = STATE.RUN;
        agent.isStopped = false;

        switch (GameEnvironment.Singleton.PhaseNumber)
        {
            case 1:
                agent.speed = 6.0f;
                Debug.Log("Fase" + GameEnvironment.Singleton.PhaseNumber);
                break;
            case 2:
                agent.speed = 7.0f;
                Debug.Log("Fase" + GameEnvironment.Singleton.PhaseNumber);
                break;
            case 3:
                agent.speed = 8.0f;
                Debug.Log("Fase" + GameEnvironment.Singleton.PhaseNumber);
                break;
        }
        //Mayor velocidad que el estado patrulla
    }
    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }
    public override void Update()
    {

        agent.SetDestination(minionPos);
        if (agent.hasPath) //Si esta yendo a la posicion
        { // 
            Debug.Log("BossCorriendo");
            if (CanAttackPlayer())//Si esta cerca
            {
                nextState = new AttackBoss(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (CanSeePlayer()) //Si no vemos al jugador, volver a patrullar
            {
                nextState = new PursueBoss(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }

            if (agent.velocity == Vector3.zero && agent.remainingDistance < 1 && !agent.pathPending) //Si se detiene el agente antes de alcanzar el destino..
            {
                Debug.Log("Arreglado");
                GameEnvironment.Singleton.MinionCall = false;
                nextState = new PatrolBoss(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
        else if (agent.remainingDistance < 1 && !agent.pathPending)
        {
            GameEnvironment.Singleton.MinionCall = false;
            nextState = new PatrolBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
