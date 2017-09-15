using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


	[SerializeField] private int startHealth = 100;
	[SerializeField] private Slider healthSlider;
	[SerializeField] private GameObject explosionPlayer;
	private PlayerController playerController;
	private AudioSource audioSource;
	private int currentHealth;
	public int CurrentHealth{
		set {
			if (value < 0) {
				currentHealth = 0;
			} else {
				currentHealth = value;
			}
		}
		get {return currentHealth;}
	}



	void Start () {
		playerController = GameObject.FindObjectOfType<PlayerController>();
		audioSource = GetComponent<AudioSource>();
		currentHealth = startHealth;
	}

	public void TakeHit() {
		if (currentHealth > 0 && !playerController.Shielded) {
			currentHealth -= 20;
			healthSlider.value = currentHealth;
		}

		if (currentHealth <= 0) {
			// StartCoroutine(Die());
			Die();
		}
	}

	void Die() {
		LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		Instantiate(explosionPlayer, transform.position, Quaternion.identity);
		Destroy(gameObject);
		Debug.Log("Die");
		// yield return new WaitForSeconds(3f);
		levelManager.LoadLevel("Win Screen");
	}

	public void PowerUpHealth() {
		if (currentHealth <= 90) {
			currentHealth += 10;
		} else if (currentHealth < startHealth) {
			currentHealth = startHealth;
		}
		audioSource.Play();
		healthSlider.value = currentHealth;
	}	

}
