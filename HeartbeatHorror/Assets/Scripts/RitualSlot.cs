using UnityEngine;
using System.Collections;

public class RitualSlot : MonoBehaviour {
   public GameObject item;
   public HardCodedInv.Artifact artifact;
   private HardCodedInv inv;
  public GameObject temp;
  bool itemInSlot;

   void Awake() {
       inv = FindObjectOfType<HardCodedInv>();
   }
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.tag);

            if (!itemInSlot)
            {
                if (inv.HasRing && artifact == HardCodedInv.Artifact.Ring)
                {
                    temp = (GameObject)GameObject.Instantiate(item, gameObject.transform.position, Quaternion.identity);
                    temp.GetComponent<SphereCollider>().enabled = false;
                    itemInSlot = true;
                }
                if (inv.HasDagger && artifact == HardCodedInv.Artifact.Dagger)
                {
                    temp = (GameObject)GameObject.Instantiate(item, gameObject.transform.position, Quaternion.identity);
                    temp.GetComponent<SphereCollider>().enabled = false;
                    itemInSlot = true;
                    // gameObject.GetComponent<RitualSlot>().enabled = false;

                }
                if (inv.HasSkull && artifact == HardCodedInv.Artifact.Skull)
                {
                    temp = (GameObject)GameObject.Instantiate(item, gameObject.transform.position , Quaternion.identity);
                    temp.GetComponent<SphereCollider>().enabled = false;
                    temp.transform.rotation = Quaternion.Euler(new Vector3(270, 0, 0));

                    itemInSlot = true;
                }
                if (inv.HasGoblet && artifact == HardCodedInv.Artifact.Goblet)
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
