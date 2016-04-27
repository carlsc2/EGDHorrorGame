using UnityEngine;
using System.Collections;

public class lightFlicker : MonoBehaviour {
	private float min_intensity;
	private float max_intensity;
	public float flickerSpeed = 0.035f;

	public GameObject flameObj;
	private Material candleMat;

	private Color lightcolor;
	private Color inverted_lightcolor;
 
	new private Light light;

	private float target_intensity;

	void Start() {
		candleMat = flameObj.GetComponent<MeshRenderer>().material;
		light = GetComponent<Light>();
		min_intensity = light.intensity * 0.9f;
		max_intensity = light.intensity * 1.1f;
		lightcolor = light.color;
		inverted_lightcolor = new Color(lightcolor.b, lightcolor.g, lightcolor.r);
		StartCoroutine(flicker());
	}

	void Update() {
		Color tmp = Color.Lerp(lightcolor, inverted_lightcolor, candleMat.GetFloat("_Color"));
		light.color = Color.Lerp(tmp, Color.green, candleMat.GetFloat("_Green"));
		light.intensity = target_intensity;//Mathf.Lerp(light.intensity, target_intensity, 1f);
	}

	IEnumerator flicker() {
		while (true) {
			target_intensity = Random.Range(min_intensity, max_intensity);
			yield return new WaitForSeconds(flickerSpeed);
		}
	}
}
