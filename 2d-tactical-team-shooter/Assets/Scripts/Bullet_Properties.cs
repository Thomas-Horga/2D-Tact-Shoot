using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet_Properties : NetworkBehaviour {

    /*
     Eventually this code might contain more information for how a bullet will behave.
    */

    public float speed; // The speed of the bullet
    public float destructionLevel; // How much destruction the bullet causes
    public bool causeDamage = true;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	}




    void OnTriggerStay2D(Collider2D coll)
    /* Triggered when a collider stays in the trigger collider. if the other collider has a gameobject with tag
    ObstacleDestrucable than cmdDestroyOther gets called.

    @rtype : None

    */
    {
        if (coll.gameObject.tag == "ObstacleDestructable")
            CmdDestroyOther(coll.gameObject);
        //CmdDestroySelf();
        
    }




    void OnCollisionEnter2D(Collision2D coll)
    /* Triggered when the obstacle's collider comes into contact with any other
    collider. Takes in the argument coll to refer to the other object that entered the collision.

    If the other object has the tag Projectile, the other object is destroyed.
    The bullets also gets "disabled" using DisableSelf() method.

    @rtype : None

    */
    {
        DisableSelf();
        if (coll.gameObject.tag == "ObstacleDestructable")
            CmdDestroyOther(coll.gameObject);
        //CmdDestroySelf();

    }



    [Command]
    void CmdDestroyOther(GameObject other)
    /* Destroy the projectile on the server.

    @type other: GameObject
        the other gameobject
   @rtype : None

   */
    {
        gameObject.GetComponent<CircleCollider2D>().radius = destructionLevel;
        Destroy(other);
        

        // May also need to remove any RPCs associated with the projectile
    }



    [Command]
    void CmdDestroySelf()
    /* Destroy the projectile on the server.

   @rtype : None

   */
    {
        NetworkServer.Destroy(gameObject);

        // May also need to remove any RPCs associated with the projectile
    }

    void DisableSelf()
    {
        /*
        Disables a bunch of components on the bullet in order to stop it from being, well, a bullet anymore

        @rtype: None
        */
        gameObject.GetComponent<CircleCollider2D>().radius = destructionLevel;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

}
