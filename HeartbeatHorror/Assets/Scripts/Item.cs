using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
    public string itemName;
    public int itemID;
    public int itemAmount;
    public itemType itemtype;
    public enum itemType { 
    
        Oil,
        KeyItem
    };
	
}
