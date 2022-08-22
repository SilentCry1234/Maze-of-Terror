using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GameObject dentro de la VictoryDoor")]
    [SerializeField] GameObject pivotPointGo;

    private PuzzleAltar puzzleAltar;

    private bool isVictoryStarted;

    private void Awake()
    {
        puzzleAltar = FindObjectOfType<PuzzleAltar>();
    }
    private void Update()
    {
        EnablePlayerVictory();
    }
    void EnablePlayerVictory()
    {
        if(puzzleAltar.PuzzleCompleted && !isVictoryStarted)
        {
            isVictoryStarted = true;
            OpenVictoryDoor();
        }
    }

    void OpenVictoryDoor()
    {
        //1-Animacion de que la puerta se cae o poner la puerta en el piso, o poner en posicion de puerta abierta
        //2-Reproducir sonido de la puerta que se cae o que se abre

        pivotPointGo.transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}