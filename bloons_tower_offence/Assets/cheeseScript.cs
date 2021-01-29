using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class cheeseScript : NetworkBehaviour
{
    public void OnCollisionEnter(Collision collision) {
        if (isServer) {
            if (collision.gameObject.tag == "Player") {
                string _id = collision.gameObject.GetComponent<NetworkIdentity>().netId.ToString();
                CustomNetworkManager.players[_id].hasCheese = true;
                
                NetworkServer.Destroy(gameObject);

            }
        }
        
    }
}
