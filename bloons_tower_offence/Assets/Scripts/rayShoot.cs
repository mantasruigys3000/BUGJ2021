using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class rayShoot : NetworkBehaviour
{
    public Camera cam;
    public float range = 100f;
    // Start is called before the first frame update

    public GameObject bulletPrefab;
    public GameObject gunEnd;

    void Start()
    {
        if (!isLocalPlayer) {
            this.enabled = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, gunEnd.transform.position, transform.rotation);
        //bullet.transform.Rotate(0, 0, 180);


        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 10000f * Time.deltaTime, ForceMode.Impulse);
        NetworkServer.Spawn(bullet);

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            


            Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.gameObject.tag == "Player") {
                string id = hit.collider.gameObject.GetComponent<NetworkIdentity>().netId.ToString();
                CustomNetworkManager.getPlayer(id).takeDamage();
            }
           
        }

    }
}
