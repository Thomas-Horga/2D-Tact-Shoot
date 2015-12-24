using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Weapon_Melee : Weapon {
    /*

        THIS CLASS STILL NEEDS TO BE IMPLEMENTED!!!!!!!!!!!!!!!!!
    */

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public override void CmdAttack()
    /*
    This class calls server side code required when attacking. It is called from within Attack.
    This class creates a bullet and instantiates it on the server.

    This function is not ment to be called directly, but rather implemented within the subclasses!

    @type target: Vector2
        The Target that the bullet will be flying towards.
    */
    {
        Debug.Log("Implement Melee!!!!");
    }
}
