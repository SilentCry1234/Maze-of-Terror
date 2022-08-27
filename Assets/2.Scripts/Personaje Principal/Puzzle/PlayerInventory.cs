using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [Header("Inventario del jugador")]
    [SerializeField] List<GameObject> inventoryGOs;

    private GameManager gameManager;
    private AudioIA audioBoss;
    public List<GameObject> InventoryGOs { get => inventoryGOs; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioBoss = FindObjectOfType<AudioIA>();
    }
    public void AddPuzzle(GameObject go)
    {
        inventoryGOs.Add(go);
        gameManager.ChangeGamePhase(InventoryGOs.Count);
    }

    public void RemovePuzzle(GameObject go)
    {
        inventoryGOs.Remove(go);
    }
}
