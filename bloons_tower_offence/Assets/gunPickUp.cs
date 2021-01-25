using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class gunPickUp : NetworkBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (isServer)
        {
            if (other.gameObject.tag == "Shotgun")
            {
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                other.gameObject.GetComponent<disableScript>().RpcDie();
                StartCoroutine(nameof(waiterBot),other.gameObject);
            }
        }
      
        
    }

    public IEnumerator waiterBot(GameObject other)
    {
        yield return new WaitForSecondsRealtime(2);
        other.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        other.gameObject.GetComponent<disableScript>().RpcRespawn();
    }
}
