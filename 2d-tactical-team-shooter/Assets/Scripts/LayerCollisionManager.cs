using UnityEngine;
using System.Collections;

public class LayerCollisionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics.IgnoreLayerCollision(8, 10, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
