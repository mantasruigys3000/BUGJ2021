using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class GameManager : NetworkBehaviour

{
 
    public void restartGame() {
        if (isServer) {
            Debug.Log("RESTARTING GAME");
            StartCoroutine(nameof(restart));
            
        }
        

    }

    public IEnumerator restart() {
        yield return new WaitForSeconds(7);
        foreach (KeyValuePair<string, NetworkPlayer> kv in CustomNetworkManager.players) {


            kv.Value.gameObject.GetComponent<movementScript>().enabled = false;

            kv.Value.isDead = false;
            kv.Value.health = 100;
            kv.Value.rayAmmo = 0;
            kv.Value.nailAmmo = 0;
            kv.Value.rocketAmmo = 0;
            kv.Value.chosenWpn = 0;
            kv.Value.points = 0;
            kv.Value.deaths = 0;


            kv.Value.gameObject.GetComponent<CharacterController>().enabled = true;
            kv.Value.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            //gameObject.GetComponent<movementScript>().enabled = true;
            kv.Value.rayGun.SetActive(false);
            kv.Value.nailGun.SetActive(false);
            kv.Value.rocketGun.SetActive(false);
            kv.Value.model.SetActive(true);
            Debug.Log("RESTART RPC CALLING");
            kv.Value.RpcRespawn();

        }

    }

}
