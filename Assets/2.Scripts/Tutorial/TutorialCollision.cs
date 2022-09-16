using System.Collections;
using UnityEngine;

public class TutorialCollision : MonoBehaviour
{
    [SerializeField] Animator boxTextAnim;

    private const string appear = "appear";
    private const string disappear = "disappear";

    private void OnTriggerEnter(Collider other)
    {
        boxTextAnim.SetBool(appear, true);
    }

    private void OnTriggerExit(Collider other)
    {
        boxTextAnim.SetBool(disappear, true);
        StartCoroutine(DisableGameObject());
    }

    IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}