using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    float health = 100f;
    [SyncVar]
    public int rayAmmo = 0;
    [SyncVar]
    public int nailAmmo = 0;
    [SyncVar]
    public int rocketAmmo = 0;
    [SyncVar]
    public int chosenWpn = 0;

    public GameObject rayGun;
    public GameObject nailGun;
    public GameObject rocketGun;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdchangeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CmdchangeWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CmdchangeWeapon(3);
        }

        if (rayAmmo > 0 && chosenWpn== 1)
        {
            rayGun.SetActive(true);
            nailGun.SetActive(false);
            rocketGun.SetActive(false);
        }

        if (nailAmmo > 0 && chosenWpn == 2)
        {
            rayGun.SetActive(false);
            nailGun.SetActive(true);
            rocketGun.SetActive(false);
        }

        if (rocketAmmo > 0 && chosenWpn == 3)
        {
            rayGun.SetActive(false);
            nailGun.SetActive(false);
            rocketGun.SetActive(true);
        }

    }
           
    [Command]
    public void CmdchangeWeapon(int weaponNr)
    {
        chosenWpn = weaponNr;
    }
    
    public void takeDamage() {
        health -= 10;
        Debug.Log(gameObject.name + " " + health.ToString());

    }


}
