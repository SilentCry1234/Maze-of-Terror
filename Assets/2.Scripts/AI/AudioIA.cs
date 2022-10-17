using System.Collections.Generic;
using UnityEngine;

public class AudioIA : MonoBehaviour
{
    [Header("Sonidos del boss")]
    [SerializeField] AudioSource bossAttackAS;
    [SerializeField] AudioSource bossGrowlAS;
    [SerializeField] AudioSource bossSurpriseAS;
    [SerializeField] AudioSource bossBreathAS;
    [Space]
    [SerializeField] AudioClip bossSurpriseAC;
    [SerializeField] AudioClip bossGrowlPhase2AC;
    [SerializeField] AudioClip bossGrowlPhase3AC;
    [Space]
    [SerializeField] List<AudioClip> bossAttacksAC;

    [Header("Sonidos del minion")]
    [SerializeField] AudioSource minionAttackAS;
    [SerializeField] AudioSource minionGrowlAS;
    [Space]
    [SerializeField] AudioClip minionGrowl;
    [SerializeField] List<AudioClip> minionAttacksAC;


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
            Destroy(gameObject);
        }
    }

    public void PlayBossBreath()
    {
        bossBreathAS.Play();
    }

    #region Attacks
    public void PlayMinionAttackSound() //Utilizado al inicio de la anim. de ataque
    {
        if (minionAttacksAC.Count == 0) return;

        int i = Random.Range(0, minionAttacksAC.Count);

        minionAttackAS.PlayOneShot(minionAttacksAC[i]);
    }

    public void PlayBossAttackSound() //Utilizado al inicio de la anim. de ataque
    {
        if (bossAttacksAC.Count == 0) return;

        int i = Random.Range(0, bossAttacksAC.Count);
        
        bossAttackAS.PlayOneShot(bossAttacksAC[i]);
    }
    #endregion

    #region Growls
    public void PlayMinionGrowlSound() //Utilizado al inicio de la anim. de ataque
    {
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