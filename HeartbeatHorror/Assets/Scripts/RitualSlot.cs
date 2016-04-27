using UnityEngine;
using System.Collections;

public class RitualSlot : MonoBehaviour {
	public GameObject item;
	public Item.itemType artifact;
	private HardCodedInv inv;
	public GameObject temp;
	bool itemInSlot;

	void Awake() {
		inv = FindObjectOfType<HardCodedInv>();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			if (!itemInSlot){
				if (inv.HasRing && artifact == Item.itemType.Ring){
					temp = (GameObject)GameObject.Instantiate(item, gameObject.transform.position, Quaternion.identity);
					temp.GetComponent<SphereCollider>().enabled = false;
					itemInSlot = true;
				}
				if (inv.HasDagger && artifact == Item.itemType.Dagger)
				{
					temp = (GameObject)GameObject.Instantiate(item, gameObject.transform.position, Quaternion.identity);
					temp.GetComponent<SphereCollider>().enabled = false;
					itemInSlot = true;
					// gameObject.GetComponent<RitualSlot>().enabled = false;

				}
				if (inv.HasSkull && artifact == Item.itemType.Skull)
				{
					temp = (GameObject)GameObject.Instantiate(item, gameObject.transform.position , Quaternion.identity);
					temp.GetComponent<SphereCollider>().enabled = false;
					temp.transform.rotation = Quaternion.Euler(new Vector3(270, 0, 0));

					itemInSlot = true;
				}
				if (inv.HasGoblet && artifact == Item.itemType.Goblet)
				{
					temp = (GameObject)GameObject.Instantiate(item, gameObject.transform.position, Quaternion.identity);
					temp.transform.rotation = Quaternion.Euler(new Vector3(270,0,0)) ;
					temp.GetComponent<SphereCollider>().enabled = false;

					itemInSlot = true;
				}

			}

		}
	}
}
