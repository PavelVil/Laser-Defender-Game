using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	[SerializeField] private float lifeTime = 5f;

	void Start () {
		Destroy(gameObject, lifeTime);
	}
	
}
