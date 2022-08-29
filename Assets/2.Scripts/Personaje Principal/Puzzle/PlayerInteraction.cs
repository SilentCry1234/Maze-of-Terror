using System;
using UnityEngine;

/// <summary>
/// Se encarga de interactuar con los GO tirando rayos desde la camara
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Distancia para agarrar")]
    [SerializeField] float interactionDist;

    [Header("Transform del GO que controla la mira")]
    [SerializeField] Transform cameraTransf;

    private PlayerInventory playerInventory;
    private PuzzleAltar puzzleAltar;

    private KeyCode interactionKey;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        puzzleAltar = FindObjectOfType<PuzzleAltar>();

        interactionKey = KeyCode.E;
    }

    private void Update()
    {
        Interact(Input.GetKeyDown(interactionKey));
    }
    void Interact(bool input) //Puede implementarse con PuzzleInteraction
    {
        if (input)
        {
            RunMethod(() => PickUpPuzzlePiece(), "PPuzzle");
            RunMethod(() => OpenAltarUI(), "Altar");
            //CastRay();
        }
    }
    //void CastRay()
    //{
    //    RaycastHit rayCastInfo;

    //    if (Physics.Raycast(cameraTransf.position, cameraTransf.forward, out rayCastInfo, interactionDist))
    //    {
    //        if (rayCastInfo.transform.gameObject.CompareTag("PPuzzle"))
    //        {
    //            rayCastInfo.transform.gameObject.SetActive(false);
    //            //AgarrarPuzzle..
    //            playerInventory.AddPuzzle(rayCastInfo.transform.gameObject);
    //        }

    //        if (rayCastInfo.transform.gameObject.CompareTag("Altar") && !puzzleAltar.PuzzleUIOpened)
    //        {
    //            puzzleAltar.ActivatePuzzleUI();
    //            StartCoroutine(puzzleAltar.SetPuzzlePiece());
    //        }
    //    }
    //    Debug.DrawRay(cameraTransf.position, cameraTransf.forward * interactionDist, Color.yellow);
    //}
    void RunMethod(Action method, string tag)
    {
        if (CompareObject(CastRay(), tag))
        {
            method();
        }
    }


    GameObject CastRay()
    {
        RaycastHit rayCastInfo;

        if (Physics.Raycast(cameraTransf.position, cameraTransf.forward, out rayCastInfo, interactionDist))
        {
            return rayCastInfo.transform.gameObject;
        }
        return null;
    }

    bool CompareObject(GameObject go, string tag)
    {
        if (go == null)
            return false;

        if (go.CompareTag(tag))
        {
            return true;
        }
        return false;
    }



    void PickUpPuzzlePiece()
    {
        GameObject go = CastRay();
        go.SetActive(false);

        playerInventory.AddPuzzle(go);
    }

    void OpenAltarUI()
    {
        if (!puzzleAltar.PuzzleUIOpened)
        {
            puzzleAltar.ActivatePuzzleUI();
            StartCoroutine(puzzleAltar.SetPuzzlePiece());
        }
    }
}