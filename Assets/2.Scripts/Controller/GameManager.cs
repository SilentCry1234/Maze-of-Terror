using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GameObject dentro de la VictoryDoor")]
    [SerializeField] GameObject pivotPointGo;
    [Space]
    [Header("Victoria")]
    [SerializeField] GameObject victoryPointGo;
    [SerializeField] float victoryDistance;
    [Space]
    [Header("Player")]
    [SerializeField] GameObject playerGo;

    private PuzzleAltar puzzleAltar;
    private AudioIA audioIA;

    private bool isVictoryStarted;

    private void Awake()
    {
        puzzleAltar = FindObjectOfType<PuzzleAltar>();
        audioIA = FindObjectOfType<AudioIA>();
    }
    private void Update()
    {
        EnablePlayerVictory();
        CheckPlayerVictory(playerGo.transform.position, victoryPointGo.transform.position);
    }
    void EnablePlayerVictory()
    {
        if (puzzleAltar.PuzzleCompleted && !isVictoryStarted)
        {
            isVictoryStarted = true;
            OpenVictoryDoor();
        }
    }

    public void ChangeGamePhase(int i) //Utilizado cuando se ejecuta el metodo AddPuzzle en PlayerInventory
    {
        switch (i)
        {
            case 3:
                GameEnvironment.Singleton.PhaseNumber = 2;
                audioIA.PlayBossGrowlPhase(GameEnvironment.Singleton.PhaseNumber);
                break;
            case 6:
                GameEnvironment.Singleton.PhaseNumber = 3;
                audioIA.PlayBossGrowlPhase(GameEnvironment.Singleton.PhaseNumber);
                break;
        }
    }

    void OpenVictoryDoor()
    {
        //1-Animacion de que la puerta se cae o poner la puerta en el piso, o poner en posicion de puerta abierta
        //2-Reproducir sonido de la puerta que se cae o que se abre

        pivotPointGo.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    void CheckPlayerVictory(Vector3 playerPos, Vector3 victoryPos)
    {
        if (Vector3.Distance(playerPos, victoryPos) < victoryDistance)
        {
            /* 1-Cartel de victoria
             * 2- Boton de ir al menu
             * */
        }
    }
}