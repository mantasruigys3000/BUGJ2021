using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class uiSync : NetworkBehaviour
{
    public GameObject health;
    public GameObject ammo;

    public bool gunPickup= false;
    public GameObject gunsIcon;
    public int chosenWpn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.GetComponent<Text>().text = gameObject.GetComponent<NetworkPlayer>().health.ToString();
        
        chosenWpn = gameObject.GetComponent<NetworkPlayer>().chosenWpn;

        if (chosenWpn==1 )
        {
            ammo.GetComponent<Text>().text = gameObject.GetComponent<NetworkPlayer>().rayAmmo.ToString();
        }
        else if (chosenWpn == 2)
        {
            ammo.GetComponent<Text>().text = gameObject.GetComponent<NetworkPlayer>().nailAmmo.ToString();
        }
        else if (chosenWpn == 3)
        {
            ammo.GetComponent<Text>().text = gameObject.GetComponent<NetworkPlayer>().rocketAmmo.ToString();
        }

        if(gunPickup)
        {
            gunsIcon.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, 2f * Time.deltaTime);

        }
    }
}
