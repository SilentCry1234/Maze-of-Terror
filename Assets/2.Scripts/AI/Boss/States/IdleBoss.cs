using UnityEngine;
using UnityEngine.AI;

public class IdleBoss : StateBoss //StateBoss
{
    public IdleBoss(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isIdle"); //Pone un "Evento" en el sistema que no se usa hasta que la anim corra, si la anim no corre o 
        base.Enter();               // no necesita ser activada, el evento sigue ahi y puede causar problemas de transicion al iniciar otro trigger
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new PursueBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 100) < 10) //Significa el 10% de las veces
        {
            nextState = new PatrolBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        //Poner un base.Update(); en esta linea esta mal porque arriba estoy declarando que voy a salir y en esta linea vuelvo al update
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}