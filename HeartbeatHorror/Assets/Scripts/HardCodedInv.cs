using UnityEngine;
using System.Collections;
/*Skull
Goblet
Dagger
Ring
*/

 public class HardCodedInv : MonoBehaviour {
     public enum Artifact
     {
         Skull = 0,
         Goblet = 1,
         Dagger = 2,
         Ring = 3

     };
     public bool HasRing, HasGoblet, HasDagger, HasSkull = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ring")
        {
            HasRing = true;
            Destroy(other.gameObject);


        }
        if (other.gameObject.tag == "Skull")
        {
            HasSkull = true;
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Goblet")
        {
            HasGoblet = true;
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Dagger")
        {
            HasDagger = true;
            Destroy(other.gameObject);
        } 
    }

}
