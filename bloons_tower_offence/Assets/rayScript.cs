using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class rayScript : NetworkBehaviour
{
    // Start is called before the first frame update

    public GameObject ray;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer) {
            ray.GetComponent<MeshRenderer>().material.color =
            new Vector4(
                 ray.GetComponent<MeshRenderer>().materials[0].color.r,
                 ray.GetComponent<MeshRenderer>().materials[0].color.g,
                 ray.GetComponent<MeshRenderer>().materials[0].color.b,
                 ray.GetComponent<MeshRenderer>().materials[0].color.a - 50f
                );

            RpcLower();

            if (ray.GetComponent<MeshRenderer>().material.color.a < 5f) {
                //NetworkServer.Destroy(gameObject);
            }

        }
    }

    [Command]
    public void Cmdlower() {
        ray.GetComponent<MeshRenderer>().material.color =
            new Vector4(
                 ray.GetComponent<MeshRenderer>().materials[0].color.r,
                 ray.GetComponent<MeshRenderer>().materials[0].color.g,
                 ray.GetComponent<MeshRenderer>().materials[0].color.b,
                 ray.GetComponent<MeshRenderer>().materials[0].color.a - 50f
                );



    }

    [ClientRpc]
    public void RpcLower() {
        ray.GetComponent<MeshRenderer>().material.color =
            new Vector4(
                 ray.GetComponent<MeshRenderer>().materials[0].color.r,
                 ray.GetComponent<MeshRenderer>().materials[0].color.g,
                 ray.GetComponent<MeshRenderer>().materials[0].color.b,
                 ray.GetComponent<MeshRenderer>().materials[0].color.a - 50f
                );
    }
}
