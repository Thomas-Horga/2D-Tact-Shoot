using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Loadout : NetworkBehaviour
{
    /*

   This Class is responsible for controlling the player's Loadout, such as which weapons are equiped and are in use

   === Public Attributes ===

   @type primaryWeaponPref : GameObject
       The Prefab that will be used for the primary weapon
   @type secondaryWeaponPref : GameObject
       The Prefab that will be used for the secondary weapon
   @type primaryWeapon : GameObject
       The instance of the player's primary weapon
   @type secondaryWeapon : GameObject
       The instance of the player's secondary weapon
   */

    public GameObject primaryWeaponPref;
    public GameObject secondaryWeaponPref;

    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;


    // Use this for initialization
    void Start () {

        CmdInitializeWeapons();
        
	}
	
	// Update is called once per frame
	void Update () {

        CmdWeaponSwitch();

    }

    [Command]
    void CmdInitializeWeapons()
    /*
    This method sets up the player's weapons when the player is spawned.
    It instantiates objects based on the given prefabs.
    
    @rtype None
    */
    {

        primaryWeapon = (GameObject)Instantiate(primaryWeaponPref, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        NetworkServer.Spawn(primaryWeapon);
        secondaryWeapon = (GameObject)Instantiate(secondaryWeaponPref, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        NetworkServer.Spawn(secondaryWeapon);
        primaryWeapon.transform.parent = transform;
        secondaryWeapon.transform.parent = transform;

        secondaryWeapon.SetActive(false);
    }

    [Command]
    void CmdWeaponSwitch()
    /*
    This method is responsible for switching the player's weapons
    Switching is controlled by the keys 1 and 2
    If the player switches to a weapon that has an empty mag, this command will
    attempt to reload the weapon.
    
    @rtype None
    */
    {
        if (Input.GetKeyDown("1"))
        {
            primaryWeapon.SetActive(true);
            secondaryWeapon.SetActive(false);

            if (primaryWeapon.GetComponent<Weapon_Ranged>().reloading)
            {
                primaryWeapon.GetComponent<Weapon_Ranged>().CmdReloadOnSwitch();
            }
        }
        if (Input.GetKeyDown("2"))
        {
            primaryWeapon.SetActive(false);
            secondaryWeapon.SetActive(true);

            if (secondaryWeapon.GetComponent<Weapon_Ranged>().reloading)
            {
                secondaryWeapon.GetComponent<Weapon_Ranged>().CmdReloadOnSwitch();
            }
        }
    }

}
