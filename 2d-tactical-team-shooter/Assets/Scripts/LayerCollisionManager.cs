using UnityEngine;
using System.Collections;

public class LayerCollisionManager : MonoBehaviour {

    /*
        Initializes some layer collisions when the game starts. required to stop players own bullets from colliding with the player.


    */

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics.IgnoreLayerCollision(8, 10, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
