using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    float health = 100f;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void takeDamage() {
        health -= 10;
        Debug.Log(gameObject.name + " " + health.ToString());

    }


}
