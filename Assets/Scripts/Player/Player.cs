using UnityEngine;
using System.Collections;

public class Player : InstanceMonoBehaviour<Player> // InstanceMonoBehaviour is so I can access the instance of this class from anywhere in the code : Chris
{
	//public GameObject LookAtTarget; // this is to point the camera at : Chris
	public CharacterController controller; 

	// temp for testing the inventory UI : Chris
	public Inventory backpack = new Inventory();
	// end temp ///////////////////////////////

	private Animator anim;
	public float mouse_sensitivity = 100;
	public float speed;
	public float turnSpeed;
	public float gravity;
	float x_rotation = 0;

	// temp for testing the inventory UI : Chris
	protected override void Awake()
	{
		base.Awake();

		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();

		backpack.Init(10 * 8);

		Cursor.lockState = CursorLockMode.Locked; // set the cursor state to hide the cursor

        for (int i = 0; i < 6; i++) // fill the backpack with junk
        {
            backpack.PutItemAt(Item.GetRandomType(), Random.Range(1, 99), i);
        }

        //for (int i = 0; i < backpack.CountItems(); i++)
        //    Debug.Log("item " + i + " is " + backpack.GetItemAt(i).type + " # " + backpack.GetItemAt(i).number);
    }
	// end temp ///////////////////////////////

	void Update()
	{
		if (PlayerBackpack.Instance.isOpen) return; // if the player backpack is open return

		float mouseX = Input.GetAxis("Mouse X") * (mouse_sensitivity * 100) * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * (mouse_sensitivity * 25) * Time.deltaTime;

		x_rotation -= mouseY;
		x_rotation = Mathf.Clamp(x_rotation, -30, 30);
		ThirdPersonCamera.Instance.transform.localRotation = Quaternion.Euler(x_rotation, 0, 0);
		transform.Rotate(Vector3.up * mouseX);

		// Reading the Input
		float x = Input.GetAxis("Horizontal") * mouse_sensitivity * Time.deltaTime;
		float y = Input.GetAxis("Vertical") * mouse_sensitivity * Time.deltaTime;

		Vector3 movement = transform.right * x + transform.forward * y;
		controller.Move(movement * speed * Time.deltaTime);

		// Moving
		if (movement.magnitude > 0)
		{
			anim.Play("infantry_combat_run", -1);
		}
		else
			anim.Play("infantry_combat_idle", -1);


        // check for key commands

        if (Input.GetKeyDown(KeyCode.Space))
        {
			Debug.Log("bring up inventory");
			PlayerBackpack.Instance.Open();
		}
    }
}
