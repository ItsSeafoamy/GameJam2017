using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private float angle;
	public float force, yForce;

	private Terrain terrain;

	private void Start() {
		terrain = FindObjectOfType<Terrain>();
		Vector3 dir = transform.position;

		int rez = terrain.terrainData.heightmapResolution;

		angle = Mathf.Atan2(-dir.z, -dir.x);
	}

	private void FixedUpdate() {
		float x = Mathf.Cos(angle) * force;
		float z = Mathf.Sin(angle) * force;

		Rigidbody rb = GetComponent<Rigidbody>();

		Vector3 dir = new Vector3(x, 0, z).normalized;
		float mag = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));
		rb.velocity = (dir * mag) + new Vector3(0, rb.velocity.y);

		float maxX = terrain.terrainData.size.x/2;
		float maxY = terrain.terrainData.size.z/2;

		if (transform.position.x > maxX || transform.position.x < -maxX || transform.position.z > maxY || transform.position.z < -maxY) {
			Player.mode = Player.Mode.MOVING_NET;
			Player.lives--;

			Animation ani = Camera.main.GetComponent<Animation>();

			if (Player.lives == 0) {
				Debug.Log("GAMEOVER!");
				Player.mode = Player.Mode.ENDGAME;

				ani.clip = ani.GetClip("CameraSad");
				ani.Play();
			} else {
				ani.clip = ani.GetClip("CameraShake");
				ani.Play();
			}

			Destroy(gameObject);
		}
	}

	public void OnCollisionEnter(Collision collision) {
		float x = Mathf.Cos(angle) * force;
		float z = Mathf.Sin(angle) * force;
		float y = collision.contacts[0].normal.y * yForce;

		Rigidbody rb = GetComponent<Rigidbody>();

		rb.AddForce(new Vector3(x, y, z));
	}
}