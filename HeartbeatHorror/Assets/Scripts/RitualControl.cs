using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RitualControl : MonoBehaviour {

	public RitualSlot slot1, slot2, slot3, slot4;
	private bool completed = false;

	public ParticleSystem rift;
	public ParticleSystem implosion;

	public GameObject dummymonster;

	public Transform playerstandpoint;

	private UnityStandardAssets.Characters.FirstPerson.MouseLook ml;

	public Transform playerlook;
	private AudioSource aso;

	// Update is called once per frame
	void Update () {
		if(!completed && slot1.itemInSlot && slot2.itemInSlot && slot3.itemInSlot && slot4.itemInSlot) {
			completed = true;
			StartCoroutine(complete_ritual());
		}
	}

	IEnumerator let_look(Transform player, Transform cam) {
		ml = new UnityStandardAssets.Characters.FirstPerson.MouseLook();
		ml.Init(player, cam);
		while (true) {
			ml.LookRotation(player, cam);
			yield return null;
		}
	}

	IEnumerator complete_ritual() {

		aso = GetComponent<AudioSource>();

		//move player to position

		Transform player = GameObject.FindGameObjectWithTag("Player").transform;		
		
		player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;

		NavMeshAgent na = player.gameObject.AddComponent<NavMeshAgent>();
		na.baseOffset = 1;

		na.SetDestination(playerstandpoint.position);

		//move player to postion
		while (true) {
			if (na.remainingDistance <= na.stoppingDistance) {
				if (!na.hasPath || na.velocity.sqrMagnitude == 0f) {
					break;
				}
			}
			yield return null;
		}

		//turn player

		Vector3 lookPos = transform.position - player.position;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation(lookPos);

		//ugly maths here because code not playing nice... but it works
		Transform pcamera = player.GetComponentInChildren<Camera>().transform;
		Vector3 lookPos3 = playerlook.position - pcamera.position;
		lookPos3.x = 0;
		Quaternion rotation3 = Quaternion.LookRotation(lookPos3);
		Vector3 tmp = rotation3.eulerAngles;
		tmp.y = 0;
		tmp.z = 0;
		//tmp.x += 240;
		rotation3 = Quaternion.Euler(tmp);

		while (Quaternion.Angle(player.rotation,rotation) > 1) {
			player.rotation = Quaternion.Slerp(player.rotation, rotation, Time.deltaTime * 2);

			
			pcamera.localRotation = Quaternion.Slerp(pcamera.localRotation, rotation3, Time.deltaTime * 2);


			yield return null;
		}

		StartCoroutine(let_look(player,pcamera));

		dummymonster.SetActive(true);

		SkinnedMeshRenderer[] skins = dummymonster.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach (SkinnedMeshRenderer skm in skins) {
			skm.enabled = false;
		}

		yield return new WaitForSeconds(1);

		foreach (SkinnedMeshRenderer skm in skins) {
			skm.enabled = true;
		}

		//open rift
		rift.randomSeed = 1;
		float i = 0;
		for(float t=0; t < 5; t += Time.deltaTime) {
			if(i < 3) {
				i = t * 3 / 5;
				rift.startSize = i;
				rift.Simulate(Time.deltaTime, false, false);
			}
			yield return new WaitForEndOfFrame();
		}
		rift.Play();
		dummymonster.GetComponentInChildren<Animator>().SetTrigger("die");


		//implode

		//implosion.Play(); //this doesnt work for some god awful reason

		//workaround.... use playonawake
		implosion.gameObject.SetActive(true);

		yield return new WaitForSeconds(1);

		//monster scream
		aso.Play();

		yield return new WaitForSeconds(0.867f);
		dummymonster.SetActive(false);

		

		rift.randomSeed = 1;
		//rift.startLifetime = 3 * 1.867f/5;
		for (float t=0; t < 0.5f; t += Time.deltaTime) {
			rift.startSize = Mathf.Lerp(3, 0, t / 0.5f);
			rift.Simulate(Time.deltaTime * 3 * 5 / 0.5f, false, false);
			yield return new WaitForEndOfFrame();
		}
		rift.startSize = 0;
		rift.Play();

		yield return new WaitForSeconds(2);

		Image blackscreen = GameObject.FindGameObjectWithTag("blackscreen").GetComponent<Image>();

		while (true) {
			blackscreen.color = Color.Lerp(blackscreen.color, Color.white, Time.deltaTime);
			if (blackscreen.color.a >= .98f) {
				SceneManager.LoadScene("youwin");
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
