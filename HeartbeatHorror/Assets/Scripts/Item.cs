using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {
	//public string itemName;
	//public int itemID;
	//public int itemAmount;
	public itemType item_type;
	public enum itemType { 
		Oil,
		Skull,
		Goblet,
		Dagger,
		Ring

	};

	void OnTriggerEnter(Collider col) {
		HardCodedInv hc = col.transform.root.GetComponent<HardCodedInv>();
		if (hc != null) {
			hc.ItemPickup(item_type);
			Destroy(gameObject);
		}
	}
	
}
