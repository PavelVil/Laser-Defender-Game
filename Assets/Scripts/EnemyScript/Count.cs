using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour {


	[SerializeField] private int countForDestroy;

	public void SetCount() {
		GameController.count += countForDestroy;
	} 

}
