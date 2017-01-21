using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GUISkin gui;

	public GameObject ball;
	public GameObject holoBall;
	public GameObject net;

	public float distance;
	public float ballHeight;

	public float netHeight;
	public float netSpeed;
	public float ballSpeed;

	public enum Mode { MOVING_NET, DROPPING_BALL, WAIT, PREGAME, ENDGAME }
	public static Mode mode = Mode.PREGAME;

	private float netTime;
	private float ballTime;

	public static int score;
	public static int highScore;

	public int maxLives;
	public static int lives;

	private void Start() {
		if (PlayerPrefs.HasKey("highscore")) {
			highScore = PlayerPrefs.GetInt("highscore");
		}

		lives = maxLives;
	}

	private void FixedUpdate() {
		if (mode == Mode.DROPPING_BALL) {
			ballTime += Time.deltaTime;

			float angle = ballTime*ballSpeed;
			Vector3 pos = holoBall.transform.position;
			pos.x = Mathf.Cos(angle)*distance;
			pos.z = Mathf.Sin(angle)*distance;
			pos.y = ballHeight;
			holoBall.transform.position = pos;

			if (Input.GetKeyDown(KeyCode.Space)) {
				float x = Mathf.Cos(angle) * distance;
				float z = Mathf.Sin(angle) * distance;

				Instantiate(ball, new Vector3(x, ballHeight, z), Quaternion.identity);

				holoBall.SetActive(false);
				mode = Mode.WAIT;
			}
		} else if (mode == Mode.MOVING_NET) {
			netTime += Time.deltaTime;

			Vector3 pos = net.transform.position;
			pos.y = Mathf.PingPong(netTime * netSpeed, netHeight) + 5;
			net.transform.position = pos;

			if (Input.GetKeyDown(KeyCode.Space)) {
				holoBall.SetActive(true);
				mode = Mode.DROPPING_BALL;
			}
		} else if (mode == Mode.WAIT) {
			//Do fuck all
		} else if (mode == Mode.PREGAME) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				mode = Mode.MOVING_NET;
			}
		} else if (mode == Mode.ENDGAME) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				mode = Mode.PREGAME;

				Animation ani = Camera.main.GetComponent<Animation>();
				ani.clip = ani.GetClip("CameraUnsad");
				ani.Play();

				score = 0;
				lives = maxLives;
			}
		}
	}

	private void OnGUI() {
		GUI.skin = gui;

		if (mode == Mode.MOVING_NET || mode == Mode.DROPPING_BALL || mode == Mode.WAIT) {
			if (score < highScore) {
				GUI.color = Color.black;
			} else {
				GUI.color = Color.HSVToRGB(Time.time%1, 1, 1);
			}

			GUI.Label(new Rect(8, 8, 1000, 32), "Score: " + score.ToString());
			GUI.Label(new Rect(8, 32, 1000, 32), "High Score: " + highScore.ToString());

			GUI.color = Color.black;
			GUI.Label(new Rect(8, 56, 1000, 32), "Lives: " + lives.ToString());
		}
	}
}