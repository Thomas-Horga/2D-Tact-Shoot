using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Basic_Shoot : NetworkBehaviour
{
    /*

    This Class is a basic Shooting template. It is very basic and should eventually be completely replaced.
    It have very basic bullet physics and instantiation, as well as very basic networking implementation

    === Public Attributes ===

    @type camera: Camera
        object represents the Camera Attached to the gameObject
    @type bullet: GameObject
        gameobject that holds a Bullet GameObject
    */

    public Camera camera;
    public GameObject bullet;
    public float fireRate;
    private float nextFire;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (isLocalPlayer)
        {
            Shoot();
        }
         
        
	}


    [Command]
    void CmdShoot()
        /*
        This class calls server side code required when shooting. It is called from within Shoot.
        This class creates a bullet and instantiates it on the server.

        @type target: Vector2
            The Target that the bullet will be flying towards.
        */
    {
        GameObject bullet_temp = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
        float speed = bullet_temp.GetComponent<Bullet_Properties>().speed;
        bullet_temp.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        NetworkServer.Spawn(bullet_temp);
        Destroy(bullet_temp, 5);
    }



    void Shoot()
        /*
         The client side code for shooting.
         This class gets the location that the player clicked at, and created a vector2 target for CmdShoot to use.
        */
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CmdShoot();
        }
    }

}
