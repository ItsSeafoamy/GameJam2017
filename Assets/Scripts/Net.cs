using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

	public void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);

		Player.mode = Player.Mode.MOVING_NET;
	}
}