using UnityEngine.AI;
using UnityEngine;

public class StunMinion : StateMinion
{
    float stunTime = 5.0f;
    bool isStunned;

    public StunMinion(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, StateBoss _boss) : base(_npc, _agent, _anim, _player, _boss)
    {
        name = STATE.STUN;
    }

    public override void Enter()
    {
        anim.SetBool("Stunned", true);
        /* anim.SetTrigger("isStuned");*/ //Anim de idle o stun
        agent.isStopped = true;
        isStunned = true;
        base.Enter();
    }

    public override void Update()
    {

        if (isStunned) // Si termino anim de STUN o termino el periodo de STUN..l
        {
            stunTime -= Time.deltaTime;
            Debug.Log("ata inmovilizado " + stunTime);
            if (stunTime <= 0.0f)
            {
                isStunned = false;
            }
        }

        else if (!isStunned && !isPlaying(anim, "MinionAttack", 0, 1.0f))
        {
            Debug.Log("retoma Patrulla");
            nextState = new PatrolMinion(npc, agent, anim, player, boss);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetBool("Stunned", false);
        //anim.ResetTrigger("isStuned");
        base.Exit();
    }
}