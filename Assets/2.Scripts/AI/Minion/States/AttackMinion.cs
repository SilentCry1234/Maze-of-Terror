using UnityEngine.AI;
using UnityEngine;

public class AttackMinion : StateMinion
{
    float attackTime = 0.25f;
    bool isAttacking;
    public AttackMinion(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, StateBoss _boss) : base(_npc, _agent, _anim, _player, _boss)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        anim.SetBool("Attacking", true);
        isAttacking = true;
        agent.isStopped = true; //Quiero detener el movimiento del npc al atacar
        
        if (AudioIA.Instance != null)
        AudioIA.Instance.PlayMinionAttackSound();
        base.Enter();
    }

    public override void Update()
    {
        if (isAttacking) // Si termino anim de ataque o termino el periodo de ataque..l
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0.0f)
            {
                isAttacking = false;
            }
        }

        if (!isAttacking)
        {
            nextState = new StunMinion(npc, agent, anim, player, boss);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetBool("Patrolling", false);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", false);
        base.Exit();
    }
}
