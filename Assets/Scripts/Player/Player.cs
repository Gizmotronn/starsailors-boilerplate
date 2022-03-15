using UnityEngine;
using System.Collections;

public class Player : InstanceMonoBehaviour<Player> // InstanceMonoBehaviour is so I can access the instance of this class from anywhere in the code : Chris
{

	// temp for testing the inventory UI : Chris
	public Inventory backpack_1 = new Inventory();
	public Inventory backpack_2 = new Inventory();
	// end temp ///////////////////////////////

	private Animator anim;
	private CharacterController controller;

	public float speed;
	public float turnSpeed;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity;

	// temp for testing the inventory UI : Chris
	protected override void Awake()
	{
		base.Awake();

		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();

		backpack_1.Init(10 * 8);
		backpack_2.Init(10 * 8);

		//for (int i = 0; i < 6; i++)
		//{
		//    backpack_1.PutItemAt(Item.GetRandomType(), Random.Range(1, 99), i);
		//    backpack_2.PutItemAt(Item.GetRandomType(), Random.Range(1, 99), i);
		//}

		//for (int i = 0; i < backpack.CountItems(); i++)
		//    Debug.Log("item " + i + " is " + backpack.GetItemAt(i).type + " # " + backpack.GetItemAt(i).number);
	}
	// end temp ///////////////////////////////

	void Update()
	{
		//if (Input.GetKey("w"))
		//{
		//	anim.SetInteger("AnimationPar", 1);
		//}
		//else
		//{
		//	anim.SetInteger("AnimationPar", 0);
		//}

		//if (controller.isGrounded)
		//{
		//	moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
		//}

		//float turn = Input.GetAxis("Horizontal");
		//transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		//controller.Move(moveDirection * Time.deltaTime);
		//moveDirection.y -= gravity * Time.deltaTime;


		// Reading the Input
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(x, 0f, y);

		// Moving
		if (movement.magnitude > 0)
		{
			movement.Normalize();
			movement *= speed * Time.deltaTime;
			transform.Translate(movement, Space.World);

			anim.Play("infantry_combat_run", -1);
		}
		else
			anim.Play("infantry_combat_idle", -1);

		// Animating
		//float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
		//float velocityX = Vector3.Dot(movement.normalized, transform.right);

		//anim.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
		//anim.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);

		AimTowardMouse();
	}

	void OnTriggerEnter(Collider other)
	{
		/* Turning this off for now : Chris
		if (other.gameObject.CompareTag("Pickup"))
		{
			other.gameObject.SetActive(false);
		}
		*/
	}

	void AimTowardMouse()
	{
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
		//{
		//	var destination = hitInfo.point;
		//	destination.y = transform.position.y;

		//	var _direction = destination - transform.position;
		//	_direction.y = 0f;
		//	_direction.Normalize();
		//	Debug.DrawRay(transform.position, _direction, Color.green);
		//	transform.rotation = Quaternion.LookRotation(_direction, transform.up);

		//	_sphere.position = destination;
		//}
	}
}
