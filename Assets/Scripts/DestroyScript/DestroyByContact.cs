using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	[SerializeField] private AudioClip destroyClip;
	[SerializeField] private GameObject explosionMeteor;
	[SerializeField] private GameObject explosionEnemy;
	private PlayerHealth playerHealth;
	private GameController gameController;

	void Start()
	{	
		gameController = GameObject.FindObjectOfType<GameController>();
		playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.CompareTag("Player")) {
			InstantiateExplosion();
			playerHealth.TakeHit();
			Destroy(gameObject);
		}
		if (target.CompareTag("PlayerLazer")) {
			
			Count count = GetComponent<Count>();
			if (count!=null) {
				AudioSource.PlayClipAtPoint(destroyClip, transform.position);
				count.SetCount();
				gameController.SetText();
			}

			InstantiateExplosion();
			
			Destroy(gameObject);
			Destroy(target.gameObject);
		}
	}

	void InstantiateExplosion() {
		if (gameObject.tag == "Meteor") {
				Instantiate(explosionMeteor, transform.position, Quaternion.identity);
			}

			if (gameObject.tag == "Enemy") {
				Instantiate(explosionEnemy, transform.position, Quaternion.identity);
			}
	}

}
