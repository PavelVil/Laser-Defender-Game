using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


	[SerializeField] private GameObject[] meteorPrefab;
	[SerializeField] private GameObject enemyFormation;
	[SerializeField] private GameObject[] powerUps;
	[SerializeField] private Text scoreText;
	[SerializeField] private int meteorsCount;
	[SerializeField] private float spawnWait;
	[SerializeField] private float startWait;
	[SerializeField] private Vector3 spawnPosition;
	[SerializeField] private int countForNextChalange = 1;
	public static int count;
	private PlayerHealth playerHealth;

	public int CountForNextChalange{
		set{countForNextChalange = value;}
		get{return countForNextChalange;}
	}
	

	void Start () {
		ResetCount();
		SetText();
		StartCoroutine(SpawnEnemies());
		StartCoroutine(SpawnPowerUps());
	}

	void Update()
	{
			
	}

	public void StartSpawnAgain() {
		StartCoroutine(SpawnEnemies());
	}

	 IEnumerator SpawnEnemies() {
		while (!Nextlevel()) {
			
			yield return new WaitForSeconds(startWait);

		for (int i = 0; i < meteorsCount; i++) {
			
			if (Nextlevel()) {
				break;
			}
			
			GameObject meteor = meteorPrefab[Random.Range(0, meteorPrefab.Length)];
			Vector3 position = new Vector3(Random.Range(-spawnPosition.x,spawnPosition.x), spawnPosition.y, spawnPosition.z);
			Instantiate(meteor, position, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(0, spawnWait));
			}
		}
		
		if(!Nextlevel()) {
			StartCoroutine(SpawnEnemies());
		}
	}

	IEnumerator SpawnPowerUps() {

		yield return new WaitForSeconds(Random.Range(5, 10));
		GameObject powerUp = powerUps[Random.Range(0,powerUps.Length)];
		Vector3 position = new Vector3(Random.Range(-spawnPosition.x,spawnPosition.x), spawnPosition.y, spawnPosition.z);
		Instantiate(powerUp, position, Quaternion.identity);

		StartCoroutine(SpawnPowerUps());
	}

	public bool Nextlevel() {
		if (count >= countForNextChalange) {
			enemyFormation.SetActive(true);
			return true;
		}
		return false;
	}

	public void SetText() {
		scoreText.text = count.ToString();
	}

	public void ResetCount() {
		count = 0;
	}
}
