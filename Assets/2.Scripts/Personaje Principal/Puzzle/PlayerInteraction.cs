using System;
using System.Collections;
using UnityEngine;
using OutlineSpace;

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

    private bool isCoroutineStarted;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        puzzleAltar = FindObjectOfType<PuzzleAltar>();

        interactionKey = KeyCode.E;
    }

    private void Update()
    {
        Interact(Input.GetKeyDown(interactionKey));
        CheckEffects();
    }
    void Interact(bool input) //Puede implementarse con PuzzleInteraction
    {
        if (input)
        {
            RunMethod(() => PickUpPuzzlePiece(), "PPuzzle");
            RunMethod(() => OpenAltarUI(), "Altar");
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

        if (go == null) return;

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
    #region Lights
    void CheckEffects()
    {
        SwitchEffects("PPuzzle");
        SwitchEffects("Battery");
    }

    void SwitchEffects(string tag)
    {
        GameObject go = CastRay();
        if (CompareObject(go, tag))
        {
            EnableLight();
            EnableOutline();
            StartCoroutine(Disable(go, tag, (go) => DisableLight(go), (go) => DisableOutline(go)));
        }
    }

    void EnableLight()
    {
        GameObject go = CastRay();

        if (go == null) return;

        Light Out = go.GetComponent<Light>();

        if (Out == null) return;

        Out.enabled = true;
    }

    IEnumerator Disable(GameObject go, string tag, Action<GameObject> ac, Action<GameObject> ac2)
    {
        if (!isCoroutineStarted)
        {
            isCoroutineStarted = true;
            while (CompareObject(go, tag))
            {
                yield return new WaitForSeconds(0.1f);
                if (!CompareObject(CastRay(), tag))
                {
                    ac(go);
                    ac2(go);
                    //DisableLight(go);
                    isCoroutineStarted = false;
                }
            }
        }
    }
    void DisableLight(GameObject go)
    {
        if (go == null) return;

        Light Out = go.GetComponent<Light>();

        if (Out == null) return;

        Out.enabled = false;
    }
    #endregion

    #region Outline

    void EnableOutline()
    {
        GameObject go = CastRay();

        if (go == null) return;

        Outline outL = go.GetComponent<Outline>();
        OutlineAnimation outA = go.GetComponent<OutlineAnimation>();

        if (outA == null) return;

        outA.enabled = true;

        if (outL == null) return;

        outL.enabled = true;



    }

    void DisableOutline(GameObject go)
    {
        if (go == null) return;

        Outline Out = go.GetComponent<Outline>();
        OutlineAnimation outA = go.GetComponent<OutlineAnimation>();

        if (Out == null) return;

        Out.enabled = false;

        if (outA == null) return;

        outA.enabled = false;
    }

    #endregion
}