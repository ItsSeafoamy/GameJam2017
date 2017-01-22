using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public enum Type { LIFE, CUP_SIZE, BOUNCE_BACK }
	public Type type;

	public float torque;
	public float minHeight, maxHeight;
	public float distance;
	public float vSpeed, hSpeed;
	public float offset;
	public float netSizeChange;

	private void FixedUpdate() {
		Vector3 euler = transform.rotation.eulerAngles;
		euler += new Vector3(0, torque, 0);
		transform.rotation = Quaternion.Euler(euler);

		Vector3 pos = transform.position;
		pos.x = Mathf.Cos(Time.time*hSpeed + offset*Mathf.Deg2Rad) * distance;
		pos.z = Mathf.Sin(Time.time*hSpeed + offset*Mathf.Deg2Rad) * distance;
		pos.y = (Mathf.Sin(Time.time*vSpeed + offset*Mathf.Deg2Rad)+1)/2 * (maxHeight-minHeight) + minHeight;
		transform.position = pos;
	}

	public void OnTriggerEnter(Collider other) {
		if (type == Type.LIFE) {
			Player.lives++;
		} else if (type == Type.CUP_SIZE) {
			Vector3 scale = FindObjectOfType<Net>().transform.lossyScale;
			scale.x += netSizeChange;
			scale.y += netSizeChange;
			scale.z += netSizeChange;
			FindObjectOfType<Net>().transform.localScale = scale;
		}

		gameObject.SetActive(false);
	}
}