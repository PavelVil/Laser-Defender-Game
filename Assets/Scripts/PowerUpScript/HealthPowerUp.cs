using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour {

	private PlayerHealth playerHealth;
	void Start () {
		playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
	}
	
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.CompareTag("Player")) {
			playerHealth.PowerUpHealth();
			Destroy(gameObject);
		}
	}

}
