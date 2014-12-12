using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0f;
	public float damage = 10f;
	public LayerMask toHit;

	public Transform bulletTransform;
	public Transform MuzzleFlash;

	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

	float timeToFire = 0f;
	Transform firePont;

	// Use this for initialization
	void Awake () 
	{
		firePont = transform.FindChild("FirePoint");
		if(firePont == null)
			Debug.LogError("Fire Point not found.");

	}
	
	// Update is called once per frame
	void Update () 
	{
		// regular shooting
		if(fireRate == 0)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				Shoot();
			}
		}

		// burst shooting
		else
		{
			if(Input.GetButton("Fire1") && Time.time > timeToFire)
			{
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}

	void Shoot()
	{
		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(worldPoint.x, worldPoint.y);
		Vector2 firePointPos = new Vector2(firePont.position.x, firePont.position.y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePos-firePointPos, 100f, toHit);

		if(Time.time >= timeToSpawnEffect)
		{
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}

		//Debug.DrawLine(firePointPos, mousePos, Color.green);
	}

	void Effect()
	{
		// trail
		Instantiate(bulletTransform, firePont.position, firePont.rotation);

		// muzzle flash
		Transform clone = (Transform)Instantiate(MuzzleFlash, firePont.position, firePont.rotation);
		clone.parent = firePont;
		float size = Random.Range(0.6f, 0.9f);
		clone.localScale = new Vector3(size, size, size);
		Destroy(clone.gameObject, 0.02f);
	}
}
