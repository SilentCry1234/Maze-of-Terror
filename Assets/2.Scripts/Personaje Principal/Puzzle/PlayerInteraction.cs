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

    private GameManager gameManager;
    private PlayerInventory playerInventory;
    private PuzzleAltar puzzleAltar;
    private AudioManager audioManager;
    private Flashlight flashLight;

    private KeyCode interactionKey;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        audioManager = FindObjectOfType<AudioManager>();
        puzzleAltar = FindObjectOfType<PuzzleAltar>();
        flashLight = FindObjectOfType<Flashlight>();

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
            RunMethod(() => PickUpBattery(), "Battery");
            RunMethod(() => InteractAltar(), "Altar");
        }
    }
    void RunMethod(Action method, string tag)
    {
        if (CompareObject(CastRay(), tag))
        {
            method();
        }
    }

    public GameObject CastRay()
    {
        RaycastHit rayCastInfo;

        if (Physics.Raycast(cameraTransf.position, cameraTransf.forward, out rayCastInfo, interactionDist))
        {
            return rayCastInfo.transform.gameObject;
        }
        return null;
    }

    public bool CompareObject(GameObject go, string tag)
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
        PlayPickUpSound();
    }

    void InteractAltar()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("Lack GameManager Script in Scene");
            return;
        }
        if(puzzleAltar == null)
        {
            Debug.LogWarning("Lack PuzzleAltar Script in Scene");
            return;
        }

        if(puzzleAltar.IsFirstInteraction)
        {
            puzzleAltar.IsFirstInteraction = false;
            gameManager.IsGameStarted = true;
            puzzleAltar.PlayScreamSound();
            return;
        }
        OpenAltarUI();
    }

    void OpenAltarUI()
    {
        if (!puzzleAltar.PuzzleUIOpened)
        {
            puzzleAltar.ActivatePuzzleUI();
            StartCoroutine(puzzleAltar.SetPuzzlePiece());
        }
    }

    void PickUpBattery()
    {
        GameObject go = CastRay();

        if (go == null) return;

        go.SetActive(false);

        if (flashLight == null) Debug.LogWarning("Falta asignar FlashLight Script en PlayerInteraction");

        flashLight.ChargeBattery();
        PlayPickUpSound();
    }

    void PlayPickUpSound()
    {
        if (audioManager == null) return;
        if (audioManager.playerPickAC == null) Debug.LogWarning("Falta asignar playerPickUp AudioClip en AudioManager");

        float newPitch = UnityEngine.Random.Range(1f, 2f);
        audioManager.ChangeASPitch(audioManager.playerDeath, newPitch);
        audioManager.PlayOneShoot(audioManager.playerDeath, audioManager.playerPickAC, 1.0f);

    }

}