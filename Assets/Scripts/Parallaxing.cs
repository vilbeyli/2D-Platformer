using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;		// stuff to be parallaxed
	private float[] parallaxScales;		// camera's movement / background movement
	public float smoothing = 1f;

	private Transform cam;				// main cam transform
	private Vector3 previousCamPos;		// position of cam in previous frame
	
	void Awake()
	{
		// set up references
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () 
	{
		// the previous frame had the current frame's cam position
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];
		for(int i = 0; i < backgrounds.Length ; ++i)
			parallaxScales[i] = backgrounds[i].position.z * -1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < backgrounds.Length ; ++i)
		{
			// the parallax is the opposite of the cam movement because the prev. frame is mult. by the cale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// set a target x pos which is the current pos + parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// create a target pos which is the backgrounds current pos with its target x pos
			Vector3 backgroundTargetPos = 
				new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current pos and the target position using lerp
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing*Time.deltaTime);
		}

		// set the previousCamPs to the cams pos at the end of the frame
		previousCamPos = cam.position;
	}
}
