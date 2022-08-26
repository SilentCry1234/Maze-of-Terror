using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [Header("Distancia para agarrar")]
    [SerializeField] float interactionDist;

    [Header("Transform del GO que controla la mira")]
    [SerializeField] Transform cameraTransf;

    [Header("Tecla Para Agarrar")]
    [SerializeField] KeyCode interactionKey;

    private PlayerInventory playerInventory;
    private PuzzleAltar puzzleAltar;

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
            CastRay();
        }
    }

    void CastRay()
    {
        RaycastHit rayCastInfo;

        if (Physics.Raycast(cameraTransf.position, cameraTransf.forward, out rayCastInfo, interactionDist))
        {
            if (rayCastInfo.transform.gameObject.CompareTag("PPuzzle"))
            {
                rayCastInfo.transform.gameObject.SetActive(false);
                //AgarrarPuzzle..
                playerInventory.AddPuzzle(rayCastInfo.transform.gameObject);
            }

            if (rayCastInfo.transform.gameObject.CompareTag("Altar") && !puzzleAltar.PuzzleUIOpened)
            {
                puzzleAltar.ActivatePuzzleUI();
                StartCoroutine(puzzleAltar.SetPuzzlePiece());
            }
        }
        Debug.DrawRay(cameraTransf.position, cameraTransf.forward * interactionDist, Color.yellow);
    }
}