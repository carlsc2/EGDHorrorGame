using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonActions : MonoBehaviour {

	public void Quit() {
		Application.Quit();
	}

	public void LoadScene(string scene) {
		SceneManager.LoadScene(scene);
	}
}
