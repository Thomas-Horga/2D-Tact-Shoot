using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class Weapon : NetworkBehaviour
{

    /*

    This Class is the extension of the Weapon template for a Ranged Weapon.

    === Public Attributes ===

    @type camera: Camera
        object represents the Camera Attached to the gameObject
    @type bullet: GameObject
        gameobject that holds a Bullet GameObject
    @type attackSpeed
        The amount of time taken to perform each attack.
    */

    // === Private Attributes ===
    // @type nextAttack : float
    //      The time at which the next attack can be performed, represented as a float.

    
    public Camera camera;
    public GameObject bullet;
    public float attackSpeed;
    private float nextAttack;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            Attack();
        }
    }

    void Attack()
    /*
     The client side code for attacking.
     This class gets the location that the player clicked at, and created a vector2 target for CmdShoot to use.
    */
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            CmdAttack();
            print("asd");
        }
    }

    [Command]
    public abstract void CmdAttack();
    /*
    This class calls server side code required when attacking. It is called from within Attack.
    This class creates a bullet and instantiates it on the server.

    This function is not ment to be called directly, but rather implemented within the subclasses!

    @type target: Vector2
        The Target that the bullet will be flying towards.
    */

}
