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


    private bool mouseOverUI;
    private bool correctPuzzlePos;

    public bool CorrectPuzzlePos { get => correctPuzzlePos; }

    private void Update()
    {
        CheckSpritePos();
        CheckMouseInput();
    }
    private void RotateLeft()
    {
        objectToRotate.transform.Rotate(Vector3.forward, 90.0f);
    }
    private void RotateRight()
    {
        objectToRotate.transform.Rotate(Vector3.forward, -90.0f);
    }

    void CheckSpritePos()
    {
        //Debug.Log(this.gameObject.name + "Correct pos " + objectToRotate.transform.rotation.eulerAngles.z);
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
                break;

            default:
                correctPuzzlePos = false;
                break;
        }
        //Debug.Log(this.gameObject.name + "Correct pos " + correctPuzzlePos);
    }
    void CheckMouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && mouseOverUI)
        {
            RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && mouseOverUI)
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
}
