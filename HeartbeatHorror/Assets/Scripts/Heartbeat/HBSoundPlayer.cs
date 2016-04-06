using UnityEngine;
using System.Collections;

public class HBSoundPlayer : MonoBehaviour {

	private AudioSource aso;
	public AudioClip beat;

	// Use this for initialization
	IEnumerator Start () {
		aso = GetComponent<AudioSource>();
		while (true) {
			float duration = 1.0f / (HBListener.Instance.outPulse);
			if (duration != -1 && duration != Mathf.Infinity) {
				//max volume at 20% above base rate
				//min volume at < 5% above base rate
				float volumescale = (HBListener.Instance.outPulse - 0.05f * HBListener.Instance.base_rate) / (0.2f * HBListener.Instance.base_rate);
				aso.PlayOneShot(beat, volumescale);
				yield return new WaitForSeconds(duration * 60);
			}
			yield return null;
		}
	}

}
