using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject ball;
	public GameObject holoBall;
	public GameObject net;

	public float distance;
	public float ballHeight;

	public float netHeight;
	public float netSpeed;
	public float ballSpeed;

	public enum Mode { MOVING_NET, DROPPING_BALL, WAIT }
	public static Mode mode = Mode.MOVING_NET;

	private float netTime;
	private float ballTime;

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
			
		}
	}
}