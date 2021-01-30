using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class nailScript : NetworkBehaviour
{
    [SyncVar]
    public string shotFrom;

    private void Start() {
        StartCoroutine(nameof(destroySelf));

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            string _id = other.gameObject.GetComponent<NetworkIdentity>().netId.ToString();
            CustomNetworkManager.players[_id].takeDamage(shotFrom, 3);
            NetworkServer.Destroy(gameObject);

        }
    }

    public IEnumerator destroySelf() {
        yield return new WaitForSeconds(5);
        NetworkServer.Destroy(gameObject);

    }
}
