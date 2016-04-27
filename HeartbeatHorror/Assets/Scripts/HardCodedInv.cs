using UnityEngine;
using System.Collections;
/*Skull
Goblet
Dagger
Ring
*/

 public class HardCodedInv : MonoBehaviour {
	public bool HasRing, HasGoblet, HasDagger, HasSkull = false;

	public void ItemPickup(Item.itemType item) {
		switch (item) {
			case Item.itemType.Dagger:
				HasDagger = true;
				break;
			case Item.itemType.Ring:
				HasRing = true;
				break;
			case Item.itemType.Skull:
				HasSkull = true;
				break;
			case Item.itemType.Goblet:
				HasGoblet = true;
				break;
		}
	}
}
