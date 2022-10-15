using UnityEngine;

public class ComicController : MonoBehaviour
{
    [SerializeField] GameObject comicsImage;

    public void ActiveNextComic()
    {

    }

    public void DisableCurrentComic(Animator anim)
    {
        anim.SetBool("disappear", true);
    }
}