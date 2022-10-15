using System.Collections.Generic;
using UnityEngine;

public class ControlGameObject : MonoBehaviour
{
    [SerializeField] Animator buttonAnim;
    [Space]
    [SerializeField] List<GameObject> goToEnable;
    [SerializeField] List<GameObject> gotToDisable;
    public void EnableGO()
    {
        if (gotToDisable == null) return;

        foreach(GameObject go in goToEnable)
        { 
            go.SetActive(true);
        }
    }

    public void DisableGO()
    {
        if (gotToDisable == null) return;

        foreach (GameObject go in gotToDisable)
        {
            go.SetActive(false);
        }
    }

    public void DisableButton()
    {
        buttonAnim.SetBool("disappear", true);
    }
}