using OutlineSpace;
using System;
using System.Collections;
using UnityEngine;

public class PlayerVEffects : MonoBehaviour
{
    [Header("Animator de PanelPickup")]
    [SerializeField] Animator pickUpAnim;


    private PlayerInteraction playerInteraction;

    private bool isCoroutineStarted;
    private void Awake()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
        CheckVisualEffects();
    }
    void CheckVisualEffects()
    {
        SwitchEffects("PPuzzle");
        SwitchEffects("Battery");
        SwitchEffects("Altar");
    }


    void SwitchEffects(string tag)
    {
        if (playerInteraction == null)
        {
            Debug.LogWarning("Falta asignar PlayerInteraction script");
            return;
        }

        GameObject go = playerInteraction.CastRay();
        if (playerInteraction.CompareObject(go, tag))
        {
            EnableLight();
            EnableOutline();
            EnableUI();
            StartCoroutine(Disable(go, tag, (go) => DisableLight(go), (go) => DisableOutline(go)));
        }
    }

    #region UI
    void EnableUI()
    {
        if (pickUpAnim == null)
        {
            Debug.LogWarning("Falta Asignar animator PickUpAnim en PlayerVEffects");
            return;
        }

        pickUpAnim.SetBool("disappear", false);
        pickUpAnim.SetBool("appear", true);
    }

    void DisableUI()
    {
        if (pickUpAnim == null)
        {
            Debug.LogWarning("Falta Asignar animator PickUpAnim en PlayerVEffects");
            return;
        }
        pickUpAnim.SetBool("appear", false);
        pickUpAnim.SetBool("disappear", true);
    }
    #endregion
    #region Lights
    void EnableLight()
    {
        GameObject go = playerInteraction.CastRay();

        if (go == null) return;

        Light Out = go.GetComponent<Light>();

        if (Out == null) return;

        Out.enabled = true;
    }

    IEnumerator Disable(GameObject go, string tag, Action<GameObject> ac, Action<GameObject> ac2)
    {
        if (playerInteraction == null)
            Debug.LogWarning("Falta asignar PlayerInteraction script");

        if (!isCoroutineStarted)
        {
            isCoroutineStarted = true;
            while (playerInteraction.CompareObject(go, tag))
            {
                yield return new WaitForSeconds(0.1f);
                if (!playerInteraction.CompareObject(playerInteraction.CastRay(), tag))
                {
                    ac(go);
                    ac2(go);
                    DisableUI();

                    isCoroutineStarted = false;
                    break;
                }
            }
        }
    }

    IEnumerator Disable(GameObject go, string tag)
    {
        if (playerInteraction == null)
            Debug.LogWarning("Falta asignar PlayerInteraction script");

        if (!isCoroutineStarted)
        {
            isCoroutineStarted = true;
            while (playerInteraction.CompareObject(go, tag))
            {
                yield return new WaitForSeconds(0.1f);
                if (!playerInteraction.CompareObject(playerInteraction.CastRay(), tag))
                {
                    DisableUI();

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
        GameObject go = playerInteraction.CastRay();

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