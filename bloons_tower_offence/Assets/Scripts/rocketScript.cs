using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class rocketScript : NetworkBehaviour
{
    [SyncVar]
    public string shotFrom;

    List<string> shot;
    private audioSync audioSync;

    public GameObject particles;

    private void Start()
    {
        audioSync = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<audioSync>();
        Debug.Log("sTEP1");
    }

    private void OnTriggerEnter(Collider other) {
        shot = new List<string>();
        if (isServer && other.gameObject.tag != "playerRocketGun") {
            Debug.Log("sTEP2");
            RpcPlaySound();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                GameObject p = Instantiate(particles, hit.point, Quaternion.identity);
                NetworkServer.Spawn(p);
            }
            

            Collider[] hits = Physics.OverlapSphere(transform.position, 7f);
            foreach(var c in hits) {
                //&& c.gameObject.GetComponent<NetworkIdentity>().netId.ToString() != shotFrom
                if (c.gameObject.tag == "Player" ){

                    if(!shot.Contains(c.gameObject.GetComponent<NetworkIdentity>().netId.ToString())) {
                    shot.Add(c.gameObject.GetComponent<NetworkIdentity>().netId.ToString());

                    int damage = 80;
                     damage -= (int) Mathf.Floor(Vector3.Distance(transform.position,c.gameObject.transform.position)) * 10;
                    
                    Debug.Log(c.gameObject.name + " EXPLODES for" + damage.ToString());
                    string hitId = c.gameObject.GetComponent<NetworkIdentity>().netId.ToString();

                    CustomNetworkManager.players[hitId].takeDamage(shotFrom, damage);

                    }
                }
            }
            NetworkServer.Destroy(gameObject);
        }    
    }

    void OnDrawGizmosSelected() {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position, 20f);
            }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 5f);
    }


    [ClientRpc]
    void RpcPlaySound()
    {
        Debug.Log("sTEP3");
        audioSync.playSound(2);
    }

}
