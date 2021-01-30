using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class cheeseDeposit : NetworkBehaviour
{
    public int forPlayer;

    

    private void OnTriggerEnter(Collider collision) {
        if (isServer) {
            if (collision.gameObject.tag == "Player") {
                Debug.Log("Player touching");
                if (collision.gameObject.GetComponent<NetworkPlayer>().hasCheese && collision.gameObject.GetComponent<NetworkPlayer>().playerIndex == forPlayer) {
                    Debug.Log("Player scored");
                    string _id = collision.gameObject.GetComponent<NetworkIdentity>().netId.ToString();
                    CustomNetworkManager.players[_id].hasCheese = false;
                    CustomNetworkManager.players[_id].addPoint(5);
                    CustomNetworkManager.SpawnCheese();

                    
                }
            }
        }
        
    }

    [Command]
    public void CmdCheeseDepsoit(string _id) {

    }
}
