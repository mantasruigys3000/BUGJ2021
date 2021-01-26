using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    float health = 100f;
    public int rayAmmo = 0;
    public int nailAmmo = 0;
    public int rocketAmmo = 0;

    public GameObject rayGun;
    public GameObject nailGun;
    public GameObject rocketGun;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rayAmmo > 0)
        {
            rayGun.SetActive(true);
        }
        else
        {
            rayGun.SetActive(false);
        }
        if (nailAmmo > 0)
        {
            nailGun.SetActive(true);
        }
        else
        {
            nailGun.SetActive(false);
        }
        if (rocketAmmo > 0)
        {
            rocketGun.SetActive(true);
        }
        else
        {
            rocketGun.SetActive(false);
        }


    }
            
    
    
    public void takeDamage() {
        health -= 10;
        Debug.Log(gameObject.name + " " + health.ToString());

    }


}
