using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PuzzlesGenerator : MonoBehaviour
{
    [Header("GameObject del player")]
    [SerializeField] Transform playerTransf;

    [Header("Distancias de spawn")]
    [SerializeField] float minDistPlayer;
    [SerializeField] float minDistPuzzle;

    [Space]
    [Header("Valores X")]
    [SerializeField] float minMapValueX;
    [SerializeField] float maxMapValueX;

    [Header("Valores Z")]
    [SerializeField] float minMapValueZ;
    [SerializeField] float maxMapValueZ;

    [Header("Valor Y")]
    [SerializeField] float puzzleOnFloor;

    [Space]
    [Header("Numero de piezas de puzzle")]
    [SerializeField] int puzzlePiecesAmount;
    [SerializeField] List<GameObject> puzzlesGOPrefab;

    [SerializeField] List<int> invokeTimes; //Tiempos a esperar entre la invocacion de pieza y pieza

    private GameManager gameManager;
    private CancellationTokenSource tokenSource; //Necesario para cancelar Task
    private List<GameObject> puzzlePieces;

    private bool isPuzzlesGenerated;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        tokenSource = new CancellationTokenSource();
    }

    private void Start()
    {
        GeneratePuzzlesPieces();
    }

    private void Update()
    {
        SecuencialPuzzles();
    }
    private void OnDisable()
    {
        tokenSource.Cancel();
    }

    void SetPuzzleNumber(GameObject go, int i)
    {
        PuzzlePiece pp = go.GetComponent<PuzzlePiece>();

        if (!pp)
            pp.PuzzleNumber = i + 1;
    }

    void GeneratePuzzlesPieces()
    {
        if (puzzlesGOPrefab == null)
        {
            Debug.LogWarning("Falta GO en Lista PuzzleGoPrefab");
            return;
        }

        puzzlePieces = new List<GameObject>();

        for (int i = 0; i < puzzlePiecesAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(puzzlesGOPrefab[i]);
            puzzlePieces.Add(obj);
            obj.transform.SetParent(this.transform);

            SetPuzzleNumber(obj, i);

            obj.SetActive(false);
        }
    }

    async void SecuencialPuzzles()
    {
        if(gameManager == null)
        {
            Debug.LogWarning("Lack GameManager script in scene");
            return;
        }
        if (!gameManager.IsGameStarted) return;

        if (!isPuzzlesGenerated)
        {
            isPuzzlesGenerated = true;
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (tokenSource.IsCancellationRequested) return;
                await ActivatePuzzle(puzzlePieces[i], invokeTimes[i]);
            }
        }
    }

    private async Task ActivatePuzzle(GameObject GO, int time)
    {
        float inicialTime = 0;

        while (inicialTime < time)
        {
            inicialTime += Time.deltaTime;
            await Task.Yield();
        }
        if (tokenSource.IsCancellationRequested) return;
        GO.transform.position = GeneratePuzzlePosition();
        GO.SetActive(true);
    }

    private Vector3 GeneratePuzzlePosition()
    {
        int numberCycles = 0;
        bool optimalPos = false;
        while (!optimalPos)
        {
            Vector3 newPos = GeneratePosInNavMesh();

            if (CheckPlayerDistance(newPos) && CheckPuzzleDistance(newPos) && !CanSeePlayer(newPos))
            {
                optimalPos = true;
                return newPos;
            }

            numberCycles++;
            if (numberCycles >= 500) //Si hay mucha distancia entre baterias y no hay espacio disponible, va a ejecutar WHILE infinitamente
            {
                Debug.LogWarning("BatteryGenerator: Posicion no valida, disminuir distancia");
                break; //Rompe el bucle While 
            }

        }

        return Vector3.zero;
    }

    private bool CheckPlayerDistance(Vector3 newPos)
    {
        if (Vector3.Distance(newPos, playerTransf.position) > minDistPlayer)
        {
            Debug.Log("PDistancia " + Vector3.Distance(newPos, playerTransf.position));
            return true;
        }
        return false;
    }

    private bool CheckPuzzleDistance(Vector3 newPos)
    {
        for (int i = 0; i < puzzlesGOPrefab.Count; i++)
        {
            if (Vector3.Distance(newPos, puzzlesGOPrefab[i].transform.position) < minDistPuzzle)
            {
                return false;
            }
        }
        return true;
    }
    private Vector3 GenerateRandomPos()
    {
        float xValue = Random.Range(minMapValueX, maxMapValueX);
        float ZValue = Random.Range(minMapValueZ, maxMapValueZ);
        Vector3 newPos = new Vector3(xValue, 0, ZValue);

        return newPos;
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

    public bool CanSeePlayer(Vector3 newPos)
    {
        Vector3 direction = playerTransf.position - newPos;
        RaycastHit rayCastInfo;

        if (Physics.Raycast(newPos, direction, out rayCastInfo))
        {
            if (rayCastInfo.transform.gameObject.tag == "Player") //Si es el jugador
            {
                Debug.DrawRay(newPos, direction, Color.green, 5);
                return true;
            }
        }
        Debug.DrawRay(newPos, direction, Color.blue, 5);
        return false;
    }

}