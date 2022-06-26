using UnityEngine.AI;
using UnityEngine;

public class StateMinion
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, STUN
    };
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage; //Protected significa que solo se puede acceder de esta clase o en las clases que heredan esta clase}
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected StateMinion nextState;
    protected NavMeshAgent agent;
    protected StateBoss boss;
    //----------------------------------------
    float visibleDist = 15.0f;
    float visibleAngle = 30.0f;
    float attackDist = 5f; //distancia de ataque, antigua 2.5
    float timeToChase = 7.0f;

    bool playerSeen;

    public StateMinion(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, StateBoss _boss)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        boss = _boss;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; } //Quiero que apenas entre vaya al estado de UPDATE
    public virtual void Update() { stage = EVENT.UPDATE; } //Virtual significa que puedo sobrescribir este metodo para que haga otra cosa (con override)
    public virtual void Exit() { stage = EVENT.EXIT; }

    public StateMinion Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this; //Va a retornar el mismo estado
    }

    /// <summary>
    /// Verifica si hay un estado de animacion activo
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="stateName"></param>
    /// <param name="animLayer"></param>
    /// <param name="loopTime"></param>
    /// <returns></returns>
    public bool isPlaying(Animator anim, string stateName, int animLayer, float loopTime) //Si Loop time es 1, reproduce la anim 1 vez
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < loopTime)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Verifica si esta reproduciendo un estado de animacion
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="stateName"></param>
    /// <param name="animLayer"></param>
    /// <returns></returns>
    public bool isAnimActive(Animator anim, string stateName, int animLayer) //Si Loop time es 1, reproduce la anim 1 vez
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName))
            return true;
        else
            return false;
    }


    public bool CanSeePlayer()
    {
        if (playerSeen)
        {
            timeToChase -= Time.deltaTime;
            if (timeToChase <= 0.0f)
            {
                playerSeen = false;
                return false;
            }
            return true;
        }

        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        if (direction.magnitude < visibleDist && angle < visibleAngle) //Magnitud es el largo del vector
        { //Si el largo del vector es menor que la distancia de vision y el angulo es menor que el angulo de vision
            RaycastHit rayCastInfo;
            if (Physics.Raycast(npc.transform.position, direction, out rayCastInfo))
            {
                if (rayCastInfo.transform.gameObject.tag == "Player") //Si es el jugador
                {
                    Debug.DrawRay(npc.transform.position, direction, Color.green);
                    playerSeen = true;
                    return true;
                }
            }
        }
        Debug.DrawRay(npc.transform.position, direction, Color.red);
        return false;
    }
    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position; //Direccion del npc al player
        if (direction.magnitude < attackDist)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerBehind() //Modificar metodo
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        if (direction.magnitude < 5 && angle < 30) //Distancia de 2
        {

            RaycastHit rayCastInfo;
            if (Physics.Raycast(npc.transform.position, direction, out rayCastInfo))
            {
                if (rayCastInfo.transform.gameObject.tag == "Player") //Si es el jugador
                {
                    Debug.DrawRay(npc.transform.position, direction, Color.green);
                    playerSeen = true;
                    return true;
                }
            }
            return true;
        }
        return false;
    }

}