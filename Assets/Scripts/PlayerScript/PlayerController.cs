using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField] private GameObject shot;
	[SerializeField] private float speed;
	[SerializeField] private float shotSpeed;
	[SerializeField] private float shotWait;
	[SerializeField] private ShieldPuwerUp shieldPowerUp;
	private AudioSource audioSource;
	private Slider shieldSlider;
	private bool shielded;
	private float padding = 0.5f;
	private float nextShot;
	private float xMin;
	private float xMax;
	private float shieldBurn;

	public bool Shielded {
		set {shielded = value;}
		get {return shielded;}
	}

	void Start () {

		shieldSlider = GameObject.Find("ShieldSlider").GetComponent<Slider>();
		audioSource = GetComponent<AudioSource>();

		float distance = Camera.main.transform.position.z - transform.position.z;
		Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distance));
		Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,distance));
		xMin = left.x + padding;
		xMax = right.x - padding;

		shieldBurn = shieldPowerUp.ShieldedTime;
	}
	
	void Update()
	{
		Fire();
		BurnShield();
	}

	void FixedUpdate () {
		Move();
	}

	void Move() {
		if (Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		transform.position = new Vector3 (Mathf.Clamp(transform.position.x ,xMin, xMax), transform.position.y, transform.position.z);
	}

	void Fire() {
		if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot) {
			nextShot = Time.time + shotWait;
			Vector3 offset = new Vector3 (0, 1, 0);
			GameObject lazer = Instantiate(shot, transform.position+offset, Quaternion.identity) as GameObject;
			lazer.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, shotSpeed, 0);
		}
	}

	public void EnterShield(float shieldedTime) {
		StartCoroutine(TakeShield(shieldedTime));
	}

	void BurnShield() {
		if (shielded) {
			shieldBurn -= 1 * Time.deltaTime;
			shieldSlider.value = shieldBurn;
		} else {
			shieldBurn = shieldPowerUp.ShieldedTime;
		}
	}

	IEnumerator TakeShield(float shieldedTime) {
		if (!shielded){
			audioSource.Play();
			shielded = true;
			shieldSlider.value = 100;
			yield return new WaitForSeconds(shieldedTime);
			shielded = false;
		}
	}
}
