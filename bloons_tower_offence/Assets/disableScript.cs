using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class disableScript : NetworkBehaviour
{
    public GameObject gunModel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    [ClientRpc]
    public void RpcDie()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gunModel.gameObject.SetActive(false);
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gunModel.gameObject.SetActive(true);
    }

   
    
}
