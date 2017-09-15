using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByAnnihilator : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other)
	{		
			Destroy(other.gameObject);
	}



}
