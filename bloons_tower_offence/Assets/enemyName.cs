using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class enemyName : NetworkBehaviour
{
    public GameObject cam;
    public GameObject nameTag;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CmdHoverEnemy();
    }

    [Command]
    void CmdHoverEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                nameTag.GetComponent<Text>().text = hit.collider.gameObject.GetComponent<NetworkPlayer>().playerName;
                nameTag.GetComponent<Text>().color= new Vector4(nameTag.GetComponent<Text>().color.r, nameTag.GetComponent<Text>().color.g, nameTag.GetComponent<Text>().color.b, 155);
            }
            

        }
        else
        {
            nameTag.GetComponent<Text>().color = new Vector4(nameTag.GetComponent<Text>().color.r, nameTag.GetComponent<Text>().color.g, nameTag.GetComponent<Text>().color.b, 0);
        }
        RpcHoverEnemy();
    }

    [ClientRpc]
    void RpcHoverEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                nameTag.GetComponent<Text>().text = hit.collider.gameObject.GetComponent<NetworkPlayer>().playerName;
                nameTag.GetComponent<Text>().color = new Vector4(nameTag.GetComponent<Text>().color.r, nameTag.GetComponent<Text>().color.g, nameTag.GetComponent<Text>().color.b, 155);
            }
            

        }
        else
        {
            nameTag.GetComponent<Text>().color = new Vector4(nameTag.GetComponent<Text>().color.r, nameTag.GetComponent<Text>().color.g, nameTag.GetComponent<Text>().color.b, 0);
        }
    }
}
