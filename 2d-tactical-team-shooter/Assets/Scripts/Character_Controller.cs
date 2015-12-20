using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Character_Controller : NetworkBehaviour {

    /*
        This class handles the movement of the player. It is network ready however very basic.
        Currently in only moves the character with default controls, and manages its position on the 
        server (quite poorly too)

        TODO: Lerp player position
    */


    public float SensitivityX; // Sensitivity of the X movement of the character
    public float SensitivityY; // Sensitivity of the Y movement of the character

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            Movement();
        }
         
	}


	void Movement()
        /*
        Class manages the movement of the player.

        @rtype: None
        */
	{
        float x = Input.GetAxis("Horizontal") * SensitivityX;
        float y = Input.GetAxis("Vertical") * SensitivityY;
        transform.Translate(x, y, 0);
	}


    public override void OnStartLocalPlayer()
        /*
           This code is called when a player is created in a multiplayer server
           This code will only run on the player that joins the server. For example,
           Currently this code will set your players color to blue and set your camera on, it only does 
           this for the local player, not for other players.
        */
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
        GetComponentInChildren<Camera>().enabled = true;
    }
}
