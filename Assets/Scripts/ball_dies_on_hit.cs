using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_dies_on_hit : MonoBehaviour 
{
	public GameObject Ball;

	//public AchievementManager AM;
	void OnTriggerEnter(Collider col)
	{
		
		if(col.GetComponent<Collider>().name ==  "Sphere")
		{
				
			// It is object B
			Debug.Log("object hit");
			Destroy(Ball);
		}
	}

}



