using UnityEngine.AI;
using UnityEngine;


public class AttackBoss : StateBoss
{
    float rotationSpeed = 2.0f;
    float attackTime = 0.25f;
    bool isAttacking;
    public AttackBoss(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        anim.SetBool("isAttacking", true); //anim de ataque
        agent.isStopped = true; //Quiero detener el movimiento del npc al disparar
        isAttacking = true;

        if (AudioIA.Instance != null)
            AudioIA.Instance.PlayBossAttackSound();
        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        //Quiero poner el eje Y en 0

        direction.y = 0; //Quiero rotar el personaje en su eje Y, ademas no quiero que se incline en el eje X ni Z 

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
                                                  Time.deltaTime * rotationSpeed);


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
            nextState = new StunBoss(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetBool("isPatrolling", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
        base.Exit();
    }
}
