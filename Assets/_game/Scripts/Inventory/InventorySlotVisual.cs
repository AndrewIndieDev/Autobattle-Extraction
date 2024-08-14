using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotVisual : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public InventoryItem Item;
    public Transform Visual;
    public Image Icon;
    public TMP_Text Amount;

    public bool IsFree => Item == null || Item.ID == -1;

    public bool IsItem(InventoryItem item) => Item != null && Item == item;

    public void SetItem(InventoryItem item)
    {
        Item = item;
        if (Item == null)
        {
            Visual.gameObject.SetActive(false);
            return;
        }
        Visual.gameObject.SetActive(true);
        Icon.sprite = item.Icon;
        Amount.text = item.Count.ToString();
    }

    public void UpdateItem()
    {
        if (Item == null || Item.Count <= 0)
        {
            Visual.gameObject.SetActive(false);
            return;
        }
        Icon.sprite = Item.Icon;
        Amount.text = Item.Count.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsFree) return;
        Icon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsFree) return;
        ResetIcon();

        InventorySlotVisual hoveredSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<InventorySlotVisual>();

        if (hoveredSlot != null)
        {
            // If we are holding shift, we need to split the stack instead of moving it
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    if (hoveredSlot.IsFree)
            //    {
            //        int count = Item.Count / 2; // currently it is flooring the value if it's odd.
            //        if (count <= 0) return;
            //        Debug.Log(count);
            //        InventoryItem halfStack = new InventoryItem(Item.Name, Item.Icon, Item.Cost, count);
            //        hoveredSlot.SetItem(halfStack);
            //        Item.AddCount(-count);
            //        UpdateItem();
            //    }
            //}
            // else we just move the item
            //else
            {
                if (hoveredSlot.IsFree)
                {
                    hoveredSlot.SetItem(Item);
                    SetItem(null);
                }
                else if (!hoveredSlot.IsItem(Item))
                {
                    InventoryItem temp = null;
                    foreach (var item in GameManager.MainInventory.Items)
                    {
                        if (item == Item)
                        {
                            temp = item;
                            break;
                        }
                    }
                    SetItem(hoveredSlot.Item);
                    hoveredSlot.SetItem(temp);
                    UpdateItem();
                }
            }
        }
    }

    private void ResetIcon()
    {
        Icon.transform.position = Visual.position;
    }
}
