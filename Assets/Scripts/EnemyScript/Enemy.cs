using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] private int heatlh;
	[SerializeField] private GameObject shot;
	[SerializeField] private float shotSpeed;
	[SerializeField] private float shotWait;
	private float nextShot;

	void Start () {
	}
	

	void Update () {
		Fire();
	}

	void Fire() {
		if (Time.time > nextShot) {
			nextShot = Time.time + shotWait;
			GameObject lazer = Instantiate(shot, transform.position, Quaternion.identity) as GameObject;
			lazer.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, shotSpeed, 0);
		}
	}
}
