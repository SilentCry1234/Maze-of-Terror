using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [Header("Inventario del jugador")]
    [SerializeField] List<GameObject> inventoryGOs;

    public List<GameObject> InventoryGOs { get => inventoryGOs; }
    public void AddPuzzle(GameObject go)
    {
        inventoryGOs.Add(go);
    }

    public void RemovePuzzle(GameObject go)
    {
        inventoryGOs.Remove(go);
    }
    public List<GameObject> ObtainInventory()
    {
        if (inventoryGOs != null)
            return inventoryGOs;
        else
            return null;
    }
}
