using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

	public void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);

		Player.mode = Player.Mode.MOVING_NET;
		Player.score++;

		if (Player.score > Player.highScore) {
			Player.highScore = Player.score;

			PlayerPrefs.SetInt("highscore", Player.highScore);
			PlayerPrefs.Save();
		}
	}
}