using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	public float speed;
	public float gravity;
	public float jumpHeight;
	public LayerMask ground;
	public Transform feet;
//	public AudioSource audio;
	public Text hintText;
	public Text wintText;
	public bool key;
	public bool findObjective;

	private Vector3 direction;
	private Vector3 walkingVelocity;
	private Vector3 fallingVelocity;
	private CharacterController controller;

	private GameObject camera1;
	private GameObject camerahalf;
	private GameObject camera2;
	private GameObject keycard;

	// Use this for initialization
	void Start () {
		speed = 9.0f;
		gravity = 9.8f;
		jumpHeight = 3.0f;
		direction = Vector3.zero;
		fallingVelocity = Vector3.zero;
		controller = GetComponent<CharacterController>();
//		audio = GetComponent<AudioSource>();
		hintText.text = "Try anything you can to escape this box!";
		wintText.text = " ";
		key = false;
		findObjective = false;
		camera1 = GameObject.FindWithTag("MainCamera");
		camera2 = GameObject.FindWithTag("Camera2");
		camerahalf = GameObject.FindWithTag ("camera1/2");
		keycard = GameObject.FindWithTag ("Pick UP");
		camera1.SetActive (true);
		camerahalf.SetActive (false);
		camera2.SetActive (false);
		keycard.SetActive (false);
		}
	
	// Update is called once per frame
	void Update () {
		if (camera1.activeSelf == true){// && camerahalf.activeSelf == true && camera2.activeSelf == true) {
			direction.x = Input.GetAxis ("Horizontal");
			direction.z = Input.GetAxis ("Vertical");
			direction = direction.normalized;
			walkingVelocity = direction * speed;
			controller.Move(walkingVelocity * Time.deltaTime);
		}
		if (camerahalf.activeSelf == true) {
			direction.z = -Input.GetAxis ("Horizontal");
			direction.x = Input.GetAxis ("Vertical");
			direction = direction.normalized;
			walkingVelocity = direction * speed;
			controller.Move(walkingVelocity * Time.deltaTime);
		}
		if (camera2.activeSelf == true) {
			direction.x = Input.GetAxis ("Horizontal");
			direction.z = Input.GetAxis ("Vertical");
			direction = direction.normalized;
			walkingVelocity = direction * speed;
			controller.Move(walkingVelocity * Time.deltaTime);
		}
		//direction.x = Input.GetAxis("Horizontal");
		//direction.z = Input.GetAxis("Vertical");
		//direction = direction.normalized;
		//walkingVelocity = direction * speed;
		//controller.Move(walkingVelocity * Time.deltaTime);
		if(direction != Vector3.zero) {
			transform.forward = direction;
			Debug.Log(direction);
		}
		bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
		if(isGrounded)
			fallingVelocity.y = 0f;
		else
			fallingVelocity.y -= gravity * Time.deltaTime;
		if(Input.GetButtonDown("Jump") && isGrounded) {
			//GetComponent<AudioSource>().Play();
			fallingVelocity.y = Mathf.Sqrt(gravity * jumpHeight);
		}
		controller.Move(fallingVelocity * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag ("Scene2")) {
			//this.transform.position = new Vector3 (-20.1f, 3.2f, 18.4f);
			camera1.SetActive(false);
			camera2.SetActive (false);
			camerahalf.SetActive (true);
			//hintText.text = " ";
			//wintText.text = "YOU WIN!"; //transform position and rotation of the camera to "new level" DON'T WIN	
		}
		if (other.gameObject.CompareTag ("secondScreen")) {
		//	camerahalf.SetActive (false);
			camera1.SetActive (false);
			camera2.SetActive (true);
		}
		//if touch ladder plane, allow control of the Y axis, or transform position to old level. so to be able to return to old map.
		if (other.gameObject.CompareTag ("ladder")) {
			this.transform.position = new Vector3 (-20.1f, 3.2f, 18.4f);
			camerahalf.SetActive (true);
			camera2.SetActive (false);
		}
		if (other.gameObject.CompareTag("Camera1")){
			camera1.SetActive(true);
			camerahalf.SetActive(false);
			camera2.SetActive (false);
		}
		if (other.gameObject.CompareTag ("Sewer")) {
			if (key == false) {
				hintText.text = "I need a Keycard to get out!";
				keycard.SetActive (true);
			}
			if (key == true && keycard.activeSelf == false) {
				hintText.text = " ";
				wintText.text = "YOU HAVE ESCAPED!"; //transform position and rotation of the camera to "new level" DON'T WIN
			}
		}
		if (other.gameObject.CompareTag ("Pick UP")) {
			other.gameObject.SetActive (false);
			hintText.text = "I got the Keycard, I can leave throught the sewer!";
			key = true;
		}
		//if touch the sensor plane right abover of terrain, then transform position of the camera so to change that camera angle.
	//	if(other.gameObject.CompareTag("Sewer")){
	//		this.transform.position(0, 0, 0);
	}
}
