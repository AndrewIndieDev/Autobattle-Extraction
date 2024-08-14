using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Transform shopInventoryParent;
    public ShopItemVisual visualPrefab;

    private Inventory shopInventory;

    private void Start()
    {
        shopInventory = new Inventory(100);

        shopInventory.OnInventoryUpdatedCount += UpdatedCount;
        shopInventory.OnInventoryAddItem += AddItem;
        shopInventory.OnInventoryRemoveItem += RemoveItem;

        shopInventory.AddItem(ItemDatabase.GetItemByID(0));
        shopInventory.AddItem(ItemDatabase.GetItemByID(1));
    }

    private void OnDestroy()
    {
        shopInventory.OnInventoryUpdatedCount -= UpdatedCount;
        shopInventory.OnInventoryAddItem -= AddItem;
        shopInventory.OnInventoryRemoveItem -= RemoveItem;
    }

    public void AddItem(InventoryItem item)
    {
        var visual = Instantiate(visualPrefab, shopInventoryParent);
        visual.SetItem(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        foreach (var visual in shopInventoryParent.GetComponentsInChildren<ShopItemVisual>())
        {
            if (visual.Item == item)
            {
                Destroy(visual.gameObject);
                break;
            }
        }
    }

    private void UpdatedCount(InventoryItem item)
    {
        
    }
}
