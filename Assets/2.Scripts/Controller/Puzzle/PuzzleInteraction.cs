using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprite a rotar")]
    [SerializeField] GameObject objectToRotate;

    [Header("Anim del puzzle button")]
    [SerializeField] Animator puzzleAnim;
    [Space]
    [SerializeField] int puzzleNumber;

    [SerializeField] AudioSource UIAS;
    [SerializeField] AudioClip clickAC;
    [SerializeField] AudioClip correctAC;


    private bool mouseOverUI;
    private bool correctPuzzlePos;

    public bool CorrectPuzzlePos { get => correctPuzzlePos; }

    private void Update()
    {
        CheckSpritePos();
        CheckInput(Input.GetKeyDown(KeyCode.Mouse0), Input.GetKeyDown(KeyCode.Mouse1)); //Para poder expandir el codigo, pido el input por parametro, asi no queda atado al teclado o dispositivo
    }
    private void RotateLeft()
    {
        objectToRotate.transform.Rotate(Vector3.forward, 90.0f);
        PlayInteractionSound(1.25f);
    }
    private void RotateRight()
    {
        objectToRotate.transform.Rotate(Vector3.forward, -90.0f);
        PlayInteractionSound(1f);

    }

    void CheckSpritePos()
    {
        if (correctPuzzlePos) return;

        switch (objectToRotate.transform.rotation.eulerAngles.z)
        {
            case >= 270: //Equivalente a -90
                correctPuzzlePos = false;
                break;
            case >= 180:
                correctPuzzlePos = false;
                break;
            case >= 90:
                correctPuzzlePos = false;
                break;
            case >= 0:
                correctPuzzlePos = true;
                PlayCorrectSound();
                break;

            default:
                correctPuzzlePos = false;
                break;
        }
    }
    void CheckInput(bool input1, bool input2) //Ver si se puede unificar con PlayerInteraction
    {
        if (correctPuzzlePos) return;

        if (input1 && mouseOverUI)
        {
            RotateLeft();
        }
        if (input2 && mouseOverUI)
        {
            RotateRight();
        }
    }

    public void ActivatePuzzleAnim(int puzz) //Activado en puzzle Altar
    {
        if (puzz == puzzleNumber)
        {
            SetInitialRotation();
            puzzleAnim.SetBool("Expand", true);
        }
    }

    private void SetInitialRotation()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                objectToRotate.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 1:
                objectToRotate.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 2:
                objectToRotate.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverUI = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverUI = false;
    }
    void PlayCorrectSound()
    {
        if (UIAS == null) { Debug.LogWarning("Falta AudioSource UIAS en PuzzleInteraction"); return; }
        if (correctAC == null) { Debug.LogWarning("Falta AudioClip correctAC en PuzzleInteraction"); return; }

        UIAS.PlayOneShot(correctAC);
    }

    void PlayInteractionSound(float pitch)
    {
        if (UIAS == null) { Debug.LogWarning("Falta AudioSource UIAS en PuzzleInteraction"); return; }
        if (clickAC == null) { Debug.LogWarning("Falta AudioClip clickAC en PuzzleInteraction"); return; }

        UIAS.pitch = pitch;
        UIAS.PlayOneShot(clickAC);
    }
}
