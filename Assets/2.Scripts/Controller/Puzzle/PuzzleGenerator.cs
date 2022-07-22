using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    [Header("Generador de Piezas de puzzle")]
    [SerializeField] int numberPuzzles;
    [SerializeField] GameObject puzzlePrefab;
    [SerializeField] List<GameObject> puzzlesGO;

    [Header("Valores X")]
    [SerializeField] float minMapValueX;
    [SerializeField] float maxMapValueX;

    [Header("Valores Z")]
    [SerializeField] float minMapValueZ;
    [SerializeField] float maxMapValueZ;

    [Header("Valor Y")]
    [SerializeField] float puzzleOnFloor;

    [Header("Distancia entre los puzzles")]
    [SerializeField] float minDistance;



    private void Start()
    {
        GeneratePuzzlePieces();
    }

    private Vector3 GenerateRandomPos()
    {
        float xValue = Random.Range(minMapValueX, maxMapValueX);
        float ZValue = Random.Range(minMapValueZ, maxMapValueZ);
        Vector3 newPos = new Vector3(xValue, 0, ZValue);

        return newPos;
    }

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

                Vector3 newPos = new Vector3(hit.position.x, puzzleOnFloor, hit.position.z);
                return newPos;
            }
        }
        Debug.Log("Fuera");
        return Vector3.zero;
    }
    private void GeneratePuzzlePieces()
    {
        puzzlesGO = new List<GameObject>();
        for (int i = 0; i < numberPuzzles; i++)
        {
            int numberCycles = 0;

            GameObject obj = (GameObject)Instantiate(puzzlePrefab);
            bool optimalPosition = false;

            if (puzzlesGO.Count > 0)
            {
                while (!optimalPosition) //si no tiene posicion optima, genera una nueva
                {
                    Vector3 newPos = GeneratePosInNavMesh();
                    int batteriesOk = 0;

                    for (int b = 0; b < puzzlesGO.Count; b++)
                    {
                        if (!CheckMinDistance(newPos, puzzlesGO[b].transform.position))
                        {
                            break;
                        }
                        else
                            batteriesOk++;
                    }
                    if (batteriesOk == puzzlesGO.Count)
                    {
                        optimalPosition = true;
                        obj.transform.position = newPos;
                    }

                    numberCycles++;
                    if (numberCycles >= 500) //Si hay mucha distancia entre baterias y no hay espacio disponible, va a ejecutar WHILE infinitamente
                    {
                        Debug.LogWarning("PuzzleGenerator: Posicion no valida, disminuir distancia");
                        break; //Rompe el bucle While 
                    }
                }
            }
            else
                obj.transform.position = GeneratePosInNavMesh();

            puzzlesGO.Add(obj);


            obj.transform.SetParent(this.transform);
        }
    }
}
