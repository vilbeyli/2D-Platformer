using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class Weapon : MonoBehaviour {

	public float fireRate = 0f;
	public int damage = 10;
	public LayerMask toHit;

	public Transform bulletTransform;
	public Transform MuzzleFlash;
    public Transform hitPrefab;


	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

	float timeToFire = 0f;
	Transform firePoint;

	// Use this for initialization
	void Awake () 
	{
		firePoint = transform.FindChild("FirePoint");
		if(firePoint == null)
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
		Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePos-firePointPos, 100f, toHit);

		

		Debug.DrawLine(firePointPos, (mousePos-firePointPos)*100, Color.green);
	    if (hit.collider != null)
	    {
	        Debug.DrawLine(firePointPos, hit.point, Color.red);
	        Enemy enemy = hit.collider.GetComponent<Enemy>();
	        if (enemy != null)
	        {
	            enemy.DamageEnemy(damage);
	        }
	    }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos, hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePos - firePointPos)*30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
 
            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
	}

	void Effect( Vector3 hitPos, Vector3 normal)
	{
		// trail
		Transform trail = Instantiate(bulletTransform, firePoint.position, firePoint.rotation) as Transform;
	    LineRenderer lr = trail.GetComponent<LineRenderer>();

	    if (lr != null)
	    {
	        // set positions
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
	    }

        Destroy(trail.gameObject, 0.04f);

	    if (normal != new Vector3(9999, 9999, 9999))
	    {
            Transform hitParticle = (Transform) Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, normal));
	        Destroy(hitParticle.gameObject, 1f);
        }

		// muzzle flash
		Transform clone = (Transform)Instantiate(MuzzleFlash, firePoint.position, firePoint.rotation);
		clone.parent = firePoint;
		float size = Random.Range(0.6f, 0.9f);
		clone.localScale = new Vector3(size, size, size);
		Destroy(clone.gameObject, 0.02f);
	}
}
