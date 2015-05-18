using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffset = 0;

	/*
	 * TODO: FIGURE OUT WHY THIS DOESN'T WORK
	 * 
	// Update is called once per frame
	void Update () 
	{

		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		difference.Normalize();

		float rotZ = Mathf.Atan2 (difference.y, difference.y) * Mathf.Rad2Deg;	// angle in degrees
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
	}

	*/

	void Update () 
		{
		Vector3 mousePos = Input.mousePosition;
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos = mousePos - pos;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - rotationOffset));
	}
}
