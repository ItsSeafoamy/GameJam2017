using System;
using UnityEngine;

public class ball : MonoBehaviour 
{

Vector3 End_Pos;
Vector3 Start_Pos;

float fraction_of_the_way_there;

void Start()
	{
	Start_Pos = transform.position;
	End_Pos = transform.position + new Vector3(0,0,100);
	}

void Update()
	{
	fraction_of_the_way_there += 0.01f; //Adjust this for how fast you want it to be.
	transform.position = Vector3.Lerp(Start_Pos,End_Pos,fraction_of_the_way_there);
	}
}