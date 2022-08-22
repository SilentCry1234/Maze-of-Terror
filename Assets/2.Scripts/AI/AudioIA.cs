using UnityEngine;

public class AudioIA : MonoBehaviour
{
    [Header("Sonidos del boss")]
    [SerializeField] AudioSource bossAttackAS;
    [SerializeField] AudioSource bossGrowlAS;
    [SerializeField] AudioSource bossSurpriseAS;
    [Space]
    [SerializeField] AudioClip bossAttackAC;
    [SerializeField] AudioClip bossSurpriseAC;
    [SerializeField] AudioClip bossGrowlPhase2AC;
    [SerializeField] AudioClip bossGrowlPhase3AC;

    [Header("Sonidos del minion")]
    [SerializeField] AudioSource minionAttackAS;
    [SerializeField] AudioSource minionGrowlAS;
    [Space]
    [SerializeField] AudioClip minionAttackAC;
    [SerializeField] AudioClip minionGrowl;


    private static AudioIA instance;
    public static AudioIA Instance { get => instance; }


    private void Awake()
    {
        if (AudioIA.instance == null)
        {
            AudioIA.instance = this;
        }
        else
        {
            Debug.Log("singletonDestruir");
            Destroy(gameObject);
        }
    }

    #region Attacks
    public void PlayMinionAttackSound() //Utilizado al inicio de la anim. de ataque
    {
        minionAttackAS.PlayOneShot(minionAttackAC);
    }

    public void PlayBossAttackSound() //Utilizado al inicio de la anim. de ataque
    {
        bossAttackAS.PlayOneShot(bossAttackAC);
    }
    #endregion

    #region Growls
    public void PlayMinionGrowlSound() //Utilizado al inicio de la anim. de ataque
    {
        Debug.Log("MGrowl");
        minionGrowlAS.PlayOneShot(minionGrowl);
    }
    public void PlayBossSurpriseSound()
    {
        bossSurpriseAS.PlayOneShot(bossSurpriseAC, 0.5f);
    }    
    public void PlayBossGrowlPhase(int phase) //utilizado al cambiar de fase en playerInventory
    {
        switch (phase)
        {
            case 2:
                bossGrowlAS.PlayOneShot(bossGrowlPhase2AC);
                break;

            case 3:
                bossGrowlAS.PlayOneShot(bossGrowlPhase3AC);
                break;

        }
    }
    #endregion

}