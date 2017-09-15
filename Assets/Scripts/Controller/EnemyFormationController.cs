using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationController : MonoBehaviour {

	[SerializeField] private GameObject enemyPrefab; 
	[SerializeField] private float width = 10f; 
	[SerializeField] private float height = 5f; 
	[SerializeField] private float speed = 5;
	[SerializeField] private float spawnDela = 0.5f;
	[SerializeField] private int wavesCount = 1;
	private int countForNextLevel;
	private GameController gameController;
	private bool movingRight = false;
	private float xMin = -2.5f;
	private float xMax = 2.5f;

	void Start () {
		gameController = GameObject.FindObjectOfType<GameController>();

		float distance = transform.position.z - Camera.main.transform.position.z; 
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance)); 
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance)); 
		xMin = leftmost.x/1.5f;
		xMax = rightmost.x;

		SpawnEnemies();
	}
	

	void Update () {
		MoveFormation();

		if (countForNextLevel >= wavesCount) {
			ChangeLevel();
		}	
	}

	void ChangeLevel() {
			countForNextLevel = 0;
			wavesCount++;
			gameController.CountForNextChalange = (GameController.count * 3) / 2 + 1;
			gameController.StartSpawnAgain();
			gameObject.SetActive(false);
	}

	void SpawnEnemies() {
		if (PositionIsEmpty() != null) {
			Transform freePosition = PositionIsEmpty();
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
			Invoke("SpawnEnemies", spawnDela);						
		}
	}

	void MoveFormation() {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * height);
		
		if (leftEdgeOfFormation < xMin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xMax) {
			movingRight = false;
		}

		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);

		transform.position = new Vector3(newX, transform.position.y, transform.position.z);

		if(AllEnemyIsDead()) {
			SpawnEnemies(); 
		}
	}

	Transform PositionIsEmpty() {
		foreach(Transform position in transform) {
			if (position.childCount == 0) {
				return position;
			}
		}
		return null;
	}

	bool AllEnemyIsDead() {
		foreach(Transform enemy in transform) {
			if (enemy.childCount > 0) {
				return false;
			}
		}
		countForNextLevel++;
		return true;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f)); 
	}
}
