using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPuwerUp : MonoBehaviour {

	[SerializeField] private float shieldedTime = 10f;
	private PlayerController playerController;

	public float ShieldedTime {
		get {return shieldedTime;}
	}

	
	void Start()
	{
		playerController = GameObject.FindObjectOfType<PlayerController>();
	}


	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.CompareTag("Player")) {
			playerController.EnterShield(shieldedTime);
			Destroy(gameObject);
		}
	}

}
