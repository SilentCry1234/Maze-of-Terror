using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;

public class BatteryGenerator : MonoBehaviour
{
    [Header("Generador de pilas")]
    [SerializeField] int numberBatteries;
    [SerializeField] GameObject bateryPrefab;
    [SerializeField] List<GameObject> bateriesGO;

    [Header("Valores X")]
    [SerializeField] float minMapValueX;
    [SerializeField] float maxMapValueX;

    [Header("Valores Z")]
    [SerializeField] float minMapValueZ;
    [SerializeField] float maxMapValueZ;

    [Header("Valor Y")]
    [SerializeField] float batteryOnFloor;

    [Header("Distancia entre pilas")]
    [SerializeField] float minDistance;


    private GameManager gameManager;

    private bool wereBatteriesGenerated;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        //GenerateBatteries();
    }
    private void Update()
    {
        GenerateBatteries();
    }
    private Vector3 GenerateRandomPos()
    {
        float xValue = Random.Range(minMapValueX, maxMapValueX);
        float ZValue = Random.Range(minMapValueZ, maxMapValueZ);
        Vector3 newPos = new Vector3(xValue, 0, ZValue);

        return newPos;
    }

    //private void GenerateBatteries()
    //{
    //    bateriesGO = new List<GameObject>();
    //    for (int i = 0; i < numberBatteries; i++)
    //    {
    //        GameObject obj = (GameObject)Instantiate(bateryPrefab);

    //        NavMeshHit hit;
    //        if (NavMesh.SamplePosition(GenerateRandomPos(), out hit, 10f, NavMesh.AllAreas))
    //        {
    //            Vector3 newPos = new Vector3(hit.position.x, batteryOnFloor, hit.position.z);

    //            obj.transform.position = newPos;
    //            Debug.Log("Bateria ");
    //        }
    //        bateriesGO.Add(obj);

    //        obj.transform.SetParent(this.transform);
    //    }
    //}
    private bool CheckMinDistance(Vector3 thisObj, Vector3 preObj)
    {
        if (Vector3.Distance(thisObj, preObj) >= minDistance)
        {
            return true;
        }
        return false;
    }
    private Vector3 GeneratePosInNavMesh()
    {
        NavMeshHit hit;
        bool posOut = false;

        while (!posOut) //mientras no haya generado una posicion dentro del NavMeshSurface, vuelve a generar otra posicion
        {
            if (NavMesh.SamplePosition(GenerateRandomPos(), out hit, 10f, NavMesh.AllAreas)) //Si esta dentro..
            {
                posOut = true;

                Vector3 newPos = new Vector3(hit.position.x, batteryOnFloor, hit.position.z);
                return newPos;
            }
        }
        Debug.Log("Fuera");
        return Vector3.zero;
    }
    private void GenerateRotation(Transform obj)
    {
        if (obj == null) { Debug.LogWarning("Empty transform in BatteryGenerator"); return; }

        int i = Random.Range(0, 4);
        float rot = 0.0f;

        switch(i)
        {
            case 0:
                rot = 0.0f;
                break;
            case 1:
                rot = 90.0f;
                break;
            case 2:
                rot = -90.0f;
                break;
            case 3:
                rot = 180.0f;
                break;
        }

        obj.Rotate(Vector3.up , rot);
    }
    private void GenerateBatteries()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("Lack GameManager script in scene");
            return;
        }
        if (!gameManager.IsGameStarted) return;

        if (!wereBatteriesGenerated)
        {
            wereBatteriesGenerated = true;
            bateriesGO = new List<GameObject>();
            for (int i = 0; i < numberBatteries; i++)
            {
                int numberCycles = 0;

                GameObject obj = (GameObject)Instantiate(bateryPrefab);
                bool optimalPosition = false;

                if (bateriesGO.Count > 0)
                {
                    while (!optimalPosition) //si no tiene posicion optima, genera una nueva
                    {
                        Vector3 newPos = GeneratePosInNavMesh();
                        int batteriesOk = 0;

                        for (int b = 0; b < bateriesGO.Count; b++)
                        {
                            if (!CheckMinDistance(newPos, bateriesGO[b].transform.position))
                            {
                                break;
                            }
                            else
                                batteriesOk++;
                        }
                        if (batteriesOk == bateriesGO.Count)
                        {
                            optimalPosition = true;
                            //asignar rotacion
                            GenerateRotation(obj.transform);
                            obj.transform.position = newPos;
                        }

                        numberCycles++;
                        if (numberCycles >= 500) //Si hay mucha distancia entre baterias y no hay espacio disponible, va a ejecutar WHILE infinitamente
                        {
                            Debug.LogWarning("BatteryGenerator: Posicion no valida, disminuir distancia");
                            break; //Rompe el bucle While 
                        }
                    }
                }
                else
                    obj.transform.position = GeneratePosInNavMesh();

                bateriesGO.Add(obj);


                obj.transform.SetParent(this.transform);
            }
        }

    }
}