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

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();

        interactionKey = KeyCode.E;
    }

    private void Update()
    {
        Interact();
    }
    void Interact()
    {
        if (Input.GetKeyDown(interactionKey))
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
        }
        Debug.DrawRay(cameraTransf.position, cameraTransf.forward * interactionDist, Color.yellow);
    }
}