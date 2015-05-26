using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	void Start()
	{
		if (gm == null)
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
	}

	public Transform player;
	public Transform spawnPoint;
	public float spawnDelay = 2f;

	public Transform spawnPrefab;

	public IEnumerator RespawnPlayer()
	{
		audio.Play ();
		yield return new WaitForSeconds(spawnDelay);
		Instantiate(player, spawnPoint.position, spawnPoint.rotation);
		Transform clone = (Transform)Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);

		Destroy(clone.gameObject, 3f);
	}

	public static void KillPlayer(Player player)
	{
		Destroy (player.gameObject);
		gm.StartCoroutine(gm.RespawnPlayer());
	}

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
