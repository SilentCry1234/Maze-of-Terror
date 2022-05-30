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



    private void Start()
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
    private void GenerateBatteries()
    {
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