using UnityEngine;
using System.Collections;

public class PauseEsc : MonoBehaviour {
	private bool paused = false;
	public GameObject panel;
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!paused) {
				CrackTime();
			} else {
				RepairTime();
			}
		}
	}
	public void CrackTime()
    {
		Time.timeScale = 0;
		paused = true;
		panel.SetActive(true);
	}
	public void RepairTime()
    {
		Time.timeScale = 1;
		paused = false;
		panel.SetActive(false);
	}
}
