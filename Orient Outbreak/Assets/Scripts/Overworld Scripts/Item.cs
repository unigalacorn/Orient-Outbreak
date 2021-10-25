using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    #region Variables and Constructor
    [SerializeField] private ItemName itemName;
    [SerializeField] private int itemQuantity; 

    public Item(ItemName _itemName, int _itemQuantity)
    {
        itemName = _itemName;
        itemQuantity = _itemQuantity;
    }
    #endregion

    #region Public Getter/Setter
    public ItemName GetItemName() => itemName;    //itemName public getter

    public int GetItemQuantity() => itemQuantity;    //itemQuantity public getter

    public void SetItemQuantity(int _itemQuantity) => itemQuantity = _itemQuantity;     //itemQuantity public setter
    #endregion
}

public enum ItemName
{
    Facts,
    LabGown,
    Food,
    Trash
}
