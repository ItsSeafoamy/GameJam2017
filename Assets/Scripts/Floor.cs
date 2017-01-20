using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	public AnimationCurve wave;
	public int size;

	private void FixedUpdate() {
		float t = Time.time % 1;

		TerrainData data = GetComponent<Terrain>().terrainData;

		float[,] heights = new float[(int) data.size.z, (int) data.size.x];

		for (int x = 0; x < data.size.x; x++) {
			for (int y = 0; y < data.size.z; y++) {
				float radius = Mathf.Atan2(data.size.z/2 - y, data.size.x/2 - x);

				heights[y, x] = (wave.Evaluate(t + (radius/32f))) / 50f;
			}
		}

		data.SetHeights(0, 0, heights);
	}
}