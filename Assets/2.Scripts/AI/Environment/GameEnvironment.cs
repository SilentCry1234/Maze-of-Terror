using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/*Este script se encarga de:
 * 1-Obtener Los CheckPoints del boss y Minion
 * 2-Avisar si al boss si llama a un minion
 */
public sealed class GameEnvironment
{

    private static GameEnvironment instance; //Instancia unica

    private List<GameObject> bossWayPoints = new List<GameObject>();
    private List<GameObject> minionWayPoints = new List<GameObject>(); //a futuro

    private Vector3 minionPos;
    private bool minionCall;
    private int phaseNumber = 1;


    public int PhaseNumber { get { return phaseNumber; } set { phaseNumber = value; } }
    public Vector3 MinionPos { get { return minionPos; } set { minionPos = value; } }
    public bool MinionCall { get { return minionCall; } set { minionCall = value; } }
    public List<GameObject> BossWayPoints { get { return bossWayPoints; } }
    public List<GameObject> MinionWayPoints
    {
        get
        {
            return minionWayPoints;
        }
    }

    // Create singleton if it doesn't already exist and populate list with any objects found with tag set to "Checkpoint".
    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
                instance.BossWayPoints.AddRange(
                    GameObject.FindGameObjectsWithTag("BossWP"));

                instance.MinionWayPoints.AddRange(
                    GameObject.FindGameObjectsWithTag("MinionWP"));

                instance.bossWayPoints = instance.bossWayPoints.OrderBy(waypoint => waypoint.name).ToList(); // Order waypoints in ascending alphabetical order by name, so that the NPC follows them correctly.
            }
            return instance;
        }
    }

    public void NextPhase(int nextPhase)
    {
        PhaseNumber = nextPhase;
    }

    public void CallBoss(Vector3 newMinionPos)
    {
        minionCall = true;
        minionPos = newMinionPos;
    }
}
