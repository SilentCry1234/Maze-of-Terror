using UnityEngine;

public class AudioBoss : MonoBehaviour
{
    [Header("Sonidos del boss")]
    [SerializeField] AudioSource attackAS;
    [SerializeField] AudioSource growlAS;
    [Space]
    [SerializeField] AudioClip attackAC;
    [SerializeField] AudioClip growlPhase2AC;
    [SerializeField] AudioClip growlPhase3AC;




    public void PlayAttackSound() //Utilizado al inicio de la anim. de ataque
    {
        attackAS.PlayOneShot(attackAC);
    }
    public void PlayGrowlPhase(int phase) //utilizado al cambiar de fase en playerInventory
    {
        switch (phase)
        {
            case 2:
                growlAS.PlayOneShot(growlPhase2AC);
                break;

            case 3:
                growlAS.PlayOneShot(growlPhase3AC);
                break;

        }
    }


}