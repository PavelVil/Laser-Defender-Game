using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour {

	[SerializeField] private GameObject explosionOnLaser;
	private PlayerHealth playerHealth;

	void Start()
	{
		playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.CompareTag("Player")) {
			playerHealth.TakeHit();
			Instantiate(explosionOnLaser, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}	
	}

}
