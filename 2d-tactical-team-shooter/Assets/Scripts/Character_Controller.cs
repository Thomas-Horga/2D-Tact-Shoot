using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Character_Controller : NetworkBehaviour {

    /*
        This class handles the movement of the player. It is network ready however very basic.
        Currently in only moves the character with default controls, and manages its position on the 
        server (quite poorly too)

        TODO: Lerp player position.
    */


    public float SensitivityX; // Sensitivity of the X movement of the character
    public float SensitivityY; // Sensitivity of the Y movement of the character
    public GameObject camera;
    public GameObject cameraPrefab;
    private Player_Collision p_collision;

    public Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        p_collision = gameObject.GetComponent<Player_Collision>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            Movement();
            CameraFollow();
        }

         
	}


	void Movement()
        /*
        Class manages the movement of the player. Also calculates the rotation
        of the player and syncs it in multiplayer by called CmdCharacterRotation.
        Also calls CameraFollowClass

        @rtype: None
        */
	{
        float x = Input.GetAxis("Horizontal") * SensitivityX;
        float y = Input.GetAxis("Vertical") * SensitivityY;
        Vector3 movementDirection = new Vector3(x, y, 0);

        if (!p_collision.CheckWallCollision(movementDirection) && movementDirection != new Vector3(0,0,0))
        {
            //transform.position = transform.position + movementDirection;
            rigidbody.velocity = movementDirection;
        }

        CmdCharacterPosition(transform.position);

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        CmdCharacterRotation(rot_z);

        CameraFollow();
         
    }

    [Command]
    void CmdCharacterPosition(Vector3 newPosition)
        /*
            Lerps between the local player position and the network player position in order to create a smooth transition between the two
            Only does this for other players.
            @type newPosition: Vector3
        */ 
    {
        if(!isLocalPlayer)
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.15f);
    }

    [Command]
    void CmdCharacterRotation(float rot_z)
    {
        /*
        sets the player rotation accordingly and syncs it in multiplayer.


        @type rot_z: float
            The Z rotation to be set for the cahracter
        */
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    void CameraFollow()
        /*
        Makes the Camera Follow the player
        */
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
    }



    public override void OnStartLocalPlayer()
        /*
           This code is called when a player is created in a multiplayer server
           This code will only run on the player that joins the server. For example,
           Currently this code will set your players color to blue and set your camera on, it only does 
           this for the local player, not for other players.
        */
    {
        gameObject.layer = 8;
        GetComponent<SpriteRenderer>().color = Color.blue;
        //GetComponentInChildren<Camera>().enabled = true;
        Vector3 position = new Vector3(transform.position.x, transform.position.y, -10);
        camera = (GameObject)Instantiate(cameraPrefab, position, transform.rotation);
    }
}
