using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	public AnimationCurve wave;
	public float speed;
	public float scale;

	private void FixedUpdate() {
		float t = Time.time * speed;

		TerrainData data = GetComponent<Terrain>().terrainData;

		float[,] heights = new float[data.heightmapResolution, data.heightmapResolution];

		for (int x = 0; x < data.heightmapResolution; x++) {
			for (int y = 0; y < data.heightmapResolution; y++) {
				float radius = Mathf.Atan2(data.heightmapResolution/2 - y, data.heightmapResolution/2 - x);

				float dist = Vector3.Distance(new Vector3(x, y), new Vector3(data.heightmapResolution/2, data.heightmapResolution/2));

				heights[y, x] = (wave.Evaluate((t + (dist/scale))%1)) / 50f * (dist / 32f);
			}
		}

		data.SetHeights(0, 0, heights);
	}
}