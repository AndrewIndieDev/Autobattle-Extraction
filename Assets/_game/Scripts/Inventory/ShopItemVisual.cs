using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemVisual : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image Icon;
    public TMP_Text Name;
    public TMP_Text Cost;

    public InventoryItem Item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameManager.MainInventory.AddItem(new InventoryItem(Item));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // show hover effect
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // remove hover effect
    }

    public void SetItem(InventoryItem item)
    {
        Item = item;
        Icon.sprite = item.Icon;
        Name.text = item.Name;
        Cost.text = item.Cost.ToString();
    }
}
