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

	public float speed = 600.0f;
	public float turnSpeed = 400.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;

	// temp for testing the inventory UI : Chris
	protected override void Awake()
	{
		base.Awake();

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

	void Start()
	{
		/* Turning this off for now : Chris
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
		*/
	}

	void Update()
	{
		/* Turning this off for now : Chris
		if (Input.GetKey("w"))
		{
			anim.SetInteger("AnimationPar", 1);
		}
		else
		{
			anim.SetInteger("AnimationPar", 0);
		}

		if (controller.isGrounded)
		{
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
		}

		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move(moveDirection * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;
		*/
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
}
