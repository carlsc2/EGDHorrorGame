using UnityEngine;
using System.Collections;

public class createFakeTerrains : MonoBehaviour {

	public GameObject dummyMansion;

	// Use this for initialization
	void Start () {
		TerrainData t = GetComponent<Terrain>().terrainData;

		float offset = t.size.x;// / 2;

		GameObject tmp;

		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z + offset);
		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z - offset);
		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z + offset);
		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z - offset);
		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + offset);
		tmp = Terrain.CreateTerrainGameObject(t);
		tmp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - offset);	

		print(offset);
		
	}
}
