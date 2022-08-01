using UnityEngine;

public class PuzzleAltar : MonoBehaviour
{
    [Header("GameObject necesarios")]
    [SerializeField] Transform playerTransf;
    [SerializeField] Transform altarTransf;

    [Header("Distancia minima a desactivar UI")]
    [SerializeField] float minInteracDis;

    private void Update()
    {
        DisableAltarUI();
    }

    void DisableAltarUI()
    {
        if (Vector3.Distance(playerTransf.position, altarTransf.position) < minInteracDis)
        {
            //Desactivar Anim de panel
        }
    }
}