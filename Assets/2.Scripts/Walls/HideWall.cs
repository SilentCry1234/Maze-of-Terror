using UnityEngine;

public class HideWall : MonoBehaviour
{
    [SerializeField] float yPos;

    private GameManager gameManager;
    private Animator hiddenWallAnim;
    private Rigidbody wallRb;

    private bool isAnimationStart;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        hiddenWallAnim = GetComponent<Animator>();
        wallRb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        StartHideAnim();
        MoveWallDown();
    }

    private void MoveWallDown()
    {
        if (isAnimationStart)
            wallRb.MovePosition(new Vector3(wallRb.position.x, yPos, wallRb.position.z));
    }
    public void DisableWall()
    {
        this.gameObject.SetActive(false);
    }
    private void StartHideAnim()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("No gameManager script in Scene");
            return;
        }
        if (hiddenWallAnim == null)
        {
            Debug.LogWarning("HiddenWallGo without animator component");
            return;
        }
        if (gameManager.IsGameStarted)
        {
            isAnimationStart = true;
            hiddenWallAnim.SetBool("disappear", true);
        }
    }

}