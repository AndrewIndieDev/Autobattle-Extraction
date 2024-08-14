using UnityEngine;

public class InventoryVisual : MonoBehaviour
{
    public Transform inventoryParent;

    private void Start()
    {
        GameManager.MainInventory.OnInventoryUpdatedCount += UpdatedCount;
        GameManager.MainInventory.OnInventoryAddItem += AddItem;
        GameManager.MainInventory.OnInventoryRemoveItem += RemoveItem;
    }

    private void OnDestroy()
    {
        GameManager.MainInventory.OnInventoryUpdatedCount -= UpdatedCount;
        GameManager.MainInventory.OnInventoryAddItem -= AddItem;
        GameManager.MainInventory.OnInventoryRemoveItem -= RemoveItem;
    }

    private void UpdatedCount(InventoryItem item)
    {
        foreach (InventorySlotVisual slot in inventoryParent.GetComponentsInChildren<InventorySlotVisual>())
        {
            if (slot.IsItem(item))
            {
                slot.UpdateItem();
                break;
            }
        }
    }

    private void AddItem(InventoryItem item)
    {
        foreach (InventorySlotVisual slot in inventoryParent.GetComponentsInChildren<InventorySlotVisual>())
        {
            if (slot.IsFree)
            {
                slot.SetItem(item);
                break;
            }
        }
    }

    private void RemoveItem(InventoryItem item)
    {
        foreach (InventorySlotVisual slot in inventoryParent.GetComponentsInChildren<InventorySlotVisual>())
        {
            if (slot.IsItem(item))
            {
                slot.SetItem(null);
                break;
            }
        }
    }
}
