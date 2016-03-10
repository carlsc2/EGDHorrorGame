using UnityEngine;
using System.Collections;

public class lightFlicker : MonoBehaviour {
	public float min_intensity = 0.5f;
	public float max_intensity = 2.5f;
	public float flickerSpeed = 0.035f;

	public Material candleMat;

	private Color lightcolor;
	private Color inverted_lightcolor;
 
	new private Light light;

	void Start() {
		light = GetComponent<Light>();
		lightcolor = light.color;
		inverted_lightcolor = new Color(lightcolor.b, lightcolor.g, lightcolor.r);
		StartCoroutine(flicker());
	}

	void Update() {
		light.color = Color.Lerp(lightcolor, inverted_lightcolor, candleMat.GetFloat("_Color"));
	}

	IEnumerator flicker() {
		while (true) {
			light.intensity = Random.Range(min_intensity, max_intensity);
			yield return new WaitForSeconds(flickerSpeed);
		}
	}
}
