using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	public AnimationCurve wave;
	public float speed;
	public float scale;
	public float height;

	private void FixedUpdate() {
		float t = Time.time * speed;
		float h = height / 600;

		TerrainData data = GetComponent<Terrain>().terrainData;

		float[,] heights = new float[data.heightmapResolution, data.heightmapResolution];

		for (int x = 0; x < data.heightmapResolution; x++) {
			for (int y = 0; y < data.heightmapResolution; y++) {
				float dist = Vector3.Distance(new Vector3(x, y), new Vector3(data.heightmapResolution/2, data.heightmapResolution/2));

				heights[y, x] = (wave.Evaluate((t + (dist/scale)))) * h * (dist / 32f);
			}
		}

		data.SetHeights(0, 0, heights);
	}
}