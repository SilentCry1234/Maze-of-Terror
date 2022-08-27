using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    //Puede que se pueda optimizar con ScriptableObject

    [Header("Numero de pieza")]
    [SerializeField] int puzzleNumber;

    public int PuzzleNumber { get => puzzleNumber; set => puzzleNumber = value; }
}