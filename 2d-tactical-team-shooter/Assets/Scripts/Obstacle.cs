using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Obstacle : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
        /* Triggered when the obstacle's collider comes into contact with any other
        collider. Takes in the argument coll to refer to the other object that entered the collision.

        If the other object has the tag Projectile, the other object is destroyed.

        @rtype : None

        */
    {
        if (coll.gameObject.tag == "Projectile")
            CmdDestroyProjectile(coll.gameObject);

    }

    [Command]
    void CmdDestroyProjectile(GameObject projectile)
         /* Destroy the projectile on the server.

        @rtype : None

        */
    {
        NetworkServer.Destroy(projectile);
        
        // May also need to remove any RPCs associated with the projectile
    }

}
