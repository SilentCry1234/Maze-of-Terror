using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator botonAnimator;
    [SerializeField] string nombreCondicionDeBoton;

    [Header("Valor a resetear")]
    [SerializeField] string condicionAReset;
    [Header("Audios")]
    [SerializeField] AudioSource mouseAS;
    [SerializeField] AudioSource radiationAS;
    [SerializeField] AudioClip clickAC;
    [SerializeField] AudioClip screamAC;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayRadiationSound();
        botonAnimator.SetBool(nombreCondicionDeBoton, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PauseRadiationSound();
        botonAnimator.SetBool(nombreCondicionDeBoton, false);
    }

    public void DesactivarBoton(string condicionReset)
    {
        botonAnimator.SetBool(condicionReset, true);
    }

    public void DesactivarBool(string condicionReset)
    {
        botonAnimator.SetBool(condicionReset, false);
    }

    public void ResetearBool(bool estado)
    {
        botonAnimator.SetBool(condicionAReset, estado);
    }

    public void PlayClickSound()
    {
        if (mouseAS == null) return;
        mouseAS.PlayOneShot(clickAC);
    }
    public void PlayScreamSound()
    {
        if (mouseAS == null) return;
        mouseAS.PlayOneShot(screamAC);
    }

    public void PlayRadiationSound()
    {
        if (radiationAS == null) return;

        radiationAS.Play();
    }
    public void PauseRadiationSound()
    {
        if (radiationAS == null) return;

        radiationAS.Pause();
    }

}
