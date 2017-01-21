using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

	public void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);

		Player.mode = Player.Mode.MOVING_NET;
		Player.score++;

		Player.audio.PlayOneShot(Player.instance.success);

		FindObjectOfType<Floor>().s += FindObjectOfType<Floor>().speedIncrease;
		Vector3 scale = transform.lossyScale;
		scale.x -= Player.instance.netSizeChange;
		scale.y -= Player.instance.netSizeChange;
		scale.z -= Player.instance.netSizeChange;
		transform.localScale = scale;

		if (Player.score > Player.highScore) {
			Player.highScore = Player.score;

			PlayerPrefs.SetInt("highscore", Player.highScore);
			PlayerPrefs.Save();
		}
	}

	private void FixedUpdate() {
		Ball ball = FindObjectOfType<Ball>();

		if (ball != null) {
			transform.GetChild(0).LookAt(ball.transform);
		}
	}
}