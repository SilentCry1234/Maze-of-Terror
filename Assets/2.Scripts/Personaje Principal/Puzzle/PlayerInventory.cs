using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [Header("Inventario del jugador")]
    [SerializeField] List<GameObject> inventoryGOs;

    private AudioBoss audioBoss;
    public List<GameObject> InventoryGOs { get => inventoryGOs; }

    private void Awake()
    {
        audioBoss = FindObjectOfType<AudioBoss>();
    }
    public void AddPuzzle(GameObject go)
    {
        inventoryGOs.Add(go);
        ChangeGamePhase();
    }

    public void RemovePuzzle(GameObject go)
    {
        inventoryGOs.Remove(go);
    }

    private void ChangeGamePhase() //Tal vez deba ser colocado en gameManager
    {
        switch(inventoryGOs.Count)
        {
            case 3:
                GameEnvironment.Singleton.PhaseNumber = 2;
                audioBoss.PlayGrowlPhase(GameEnvironment.Singleton.PhaseNumber);
                break;
            case 6:
                GameEnvironment.Singleton.PhaseNumber = 3;
                audioBoss.PlayGrowlPhase(GameEnvironment.Singleton.PhaseNumber);
                break;
        }
    }
}
