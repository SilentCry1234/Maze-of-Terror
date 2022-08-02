using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAltar : MonoBehaviour
{
    [Header("GameObject necesarios")]
    [SerializeField] Transform playerTransf;
    [SerializeField] Transform altarTransf;

    [Header("Animator de Puzzle UI")]
    [SerializeField] Animator puzzleUIAnim;

    [Header("Distancia minima a desactivar UI")]
    [SerializeField] float minInteracDis;

    [Header("Puzzles UI")]
    [SerializeField] List<PuzzleInteraction> puzzlesInteraction;


    private PlayerInventory playerInventory;

    private bool puzzleUIOpened;
    public bool PuzzleUIOpened { get => puzzleUIOpened; }

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        DisableAltarUI();
    }

    public void ActivatePuzzleUI()
    {
        puzzleUIOpened = true;
        puzzleUIAnim.SetBool("Expand", true);
    }

    public IEnumerator SetPuzzlePiece()
    {
        yield return new WaitForSeconds(0.15f);

        if (playerInventory.InventoryGOs != null)
        {
            foreach (GameObject go in playerInventory.InventoryGOs.ToArray())
            {
                int num = go.GetComponent<PuzzlePiece>().PuzzleNumber;

                for (int i = 0; i < puzzlesInteraction.Count; i++)
                {
                    puzzlesInteraction[i].ActivatePuzzleAnim(num);
                }

                //playerInventory.RemovePuzzle(go);
            }
        }
    }


    void DisableAltarUI()
    { 
        if (Vector3.Distance(playerTransf.position, altarTransf.position) > minInteracDis)
        {
            puzzleUIAnim.SetBool("Expand", false);
            puzzleUIOpened = false;
        }
    }
}