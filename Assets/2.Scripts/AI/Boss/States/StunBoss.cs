using UnityEngine.AI;
using UnityEngine;

public class StunBoss : StateBoss
{
    float stunTime = 5.0f;
    bool isStunned;

    public StunBoss(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.STUN;
        agent.isStopped = true;

        switch (GameEnvironment.Singleton.PhaseNumber)
        {
            case 1:
                stunTime = 10.0f;
                Debug.Log("Stun Fase" + GameEnvironment.Singleton.PhaseNumber);
                break;
            case 2:
                stunTime = 8.0f;
                Debug.Log("Stun Fase" + GameEnvironment.Singleton.PhaseNumber);
                break;
            case 3:
                stunTime = 6.0f;
                Debug.Log("Stun Fase" + GameEnvironment.Singleton.PhaseNumber);
                break;
        }
    }

    public override void Enter()
    {
        anim.SetBool("isStuned", true); //Anim de idle o stun
        isStunned = true;
        base.Enter();
    }

    public override void Update()
    {

        if (isStunned) // Si termino anim de ataque o termino el periodo de ataque..l
        {
            stunTime -= Time.deltaTime;
            Debug.Log("stunTime " + stunTime);
            if (stunTime <= 0.0f)
            {
                isStunned = false;
            }
        }
        else if (!isStunned && !isAnimActive(anim, "BossAttack", 0, 1.0f)) //SI no esta inmovilizado, vuelve a patrullar
        {
            Debug.Log("CHANGE");
            nextState = new PatrolBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetBool("isStunned", false);
        base.Exit();
    }
}