﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Weapon_Ranged : Weapon {

    /*

    This Class is the extension of the Weapon template for a Ranged Weapon.

    === Public Attributes ===

    @type camera: Camera
        object represents the Camera Attached to the gameObject
    @type bullet: GameObject
        gameobject that holds a Bullet GameObject
    @type attackSpeed
        The amount of time taken to perform each attack.
    @type magazine_size : int
        The maximum amount of ammo allowed in the gun's magazine
    @type ammo: int
        The current amount of ammo
    @type reloadTime : float
        The amount of time it takes for the gun to reload
    @type reloading : bool
        The boolean is used to determine if the player is currently reloading.
    @type spray : float
        The maximum angle that the gun will spray at both to left and right, later converted to Euler Angles.
    */

    // === Private Attributes ===
    // @type nextAttack : float
    //      The time at which the next attack can be performed, represented as a float.
    // 

    // Spray is still being worked on

    public int magazine_size;
    public int ammo;
    public float reloadTime;
    //Afterwards change this back to private, or actually keep it as public?
    public bool reloading = false;

    public float spray;

	// Use this for initialization
	void Start () {
        ammo = magazine_size;
	}

    [Command]
    public override void CmdAttack()
    /*
    This method overrides the CmdAttack method from the parent class.
    This method calls server side code required when attacking. It is called from within Attack.
    This method creates a bullet and instantiates it on the server.
    */
    {
        if (!reloading)
        {
            GameObject bullet_temp = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
            float speed = bullet_temp.GetComponent<Bullet_Properties>().speed;

            Vector2 new_Velocity = transform.right * speed;

            //new_Velocity = Quaternion.Euler(Random.Range(-spray, spray), 0, 0) * new_Velocity;
            //new_Velocity = Quaternion.AngleAxis(Random.Range(spray * -1, spray), Vector3.up) * new_Velocity;
            bullet_temp.GetComponent<Rigidbody2D>().velocity = new_Velocity;
            NetworkServer.Spawn(bullet_temp);

            Physics2D.IgnoreCollision(bullet_temp.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>());

            //bullet_temp.transform.Rotate(new Vector3(0, Random.Range(-spray, spray), 0));

            Destroy(bullet_temp, 5);

            ammo--;

            if (ammo <= 0)
            {
                reloading = true;
                StartCoroutine(WaitToReload());
            }
        }
    }



    [Command]
    public void CmdReloadOnSwitch()
    /*
    This method is responsible for reloading the weapon when it is switched to and has
    an empty mag.

    It starts a coroutine that runs for the amount of time that the weapon needs to reload.

    @rtype None
    */
    {

        StartCoroutine(WaitToReload());
    }

    IEnumerator WaitToReload()
    /*
    This coroutine is used as a timer for reloading the player's weapon.
    It runs for the specified reloadTime and at the end sets the amount of ammo in the magazine
    as well as changing the reloading status to false.

    @rtype None
    */
    {
        Debug.Log("Before Reloading");
        yield return new WaitForSeconds(reloadTime);
        Debug.Log("After Reloading");
        ammo = magazine_size;
        reloading = false;
    }
}
