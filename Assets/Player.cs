using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats
	{
		public int hp = 100;

	}

	public PlayerStats playerStats = new PlayerStats();
	public float fallBoundary = -20f;

	void Update()
	{
		if(transform.position.y <= fallBoundary)
			DamagePlayer(10000);
	}

	public void DamagePlayer(int dmg)
	{
		playerStats.hp -= dmg;
		if(playerStats.hp <= 0)
		{
			GameMaster.KillPlayer(this);
		}
	}

}
