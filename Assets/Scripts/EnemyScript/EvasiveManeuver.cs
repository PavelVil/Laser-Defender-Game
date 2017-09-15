using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

	[SerializeField] private float maneuverSpeed;
	private bool movingRight;
	private float xMin;
	private float xMax;
	private float padding = 0.5f;

	void Start () {
		float distance = Camera.main.transform.position.z - transform.position.z;
		Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distance));
		Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,distance));
		xMin = left.x + padding;
		xMax = right.x - padding;

		movingRight = Random.Range(0,1) == 1 ? true : false;
	}
	

	void FixedUpdate () {
		Move();
	}

	void Move() {
		if (movingRight) {			
			transform.position += Vector3.right * maneuverSpeed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * maneuverSpeed * Time.deltaTime;
		}

		if (transform.position.x <= xMin) {
			movingRight = true;
		} else if (transform.position.x >= xMax) {
			movingRight = false;
		}
	}
}
