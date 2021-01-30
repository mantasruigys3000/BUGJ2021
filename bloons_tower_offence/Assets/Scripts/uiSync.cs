using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class uiSync : NetworkBehaviour
{
    public GameObject health;
    public GameObject ammo;
    public GameObject score;
    public GameObject deaths;

    public bool gunPickup= false;
    public GameObject gunsIcon;
    public int chosenWpn;

    public Canvas can;
    int rayAmmo;
    int nailAmmo;
    int rocketAmmo;
    public List<GameObject> icons;

    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
        {
            can.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rayAmmo = gameObject.GetComponent<NetworkPlayer>().rayAmmo;
        nailAmmo = gameObject.GetComponent<NetworkPlayer>().nailAmmo;
        rocketAmmo = gameObject.GetComponent<NetworkPlayer>().rocketAmmo;
        if (rayAmmo > 0)
        {
            icons[0].GetComponent<Image>().color = new Vector4(icons[0].GetComponent<Image>().color.r, icons[0].GetComponent<Image>().color.g, icons[0].GetComponent<Image>().color.b, 100);
        }
        else
        {
            icons[0].GetComponent<Image>().color = new Vector4(icons[0].GetComponent<Image>().color.r, icons[0].GetComponent<Image>().color.g, icons[0].GetComponent<Image>().color.b, 0);
        }

        if (nailAmmo > 0)
        {
            icons[1].GetComponent<Image>().color = new Vector4(icons[1].GetComponent<Image>().color.r, icons[1].GetComponent<Image>().color.g, icons[1].GetComponent<Image>().color.b, 100);
        }
        else
        {
            icons[1].GetComponent<Image>().color = new Vector4(icons[1].GetComponent<Image>().color.r, icons[1].GetComponent<Image>().color.g, icons[1].GetComponent<Image>().color.b, 0);
        }

        if (rocketAmmo > 0)
        {
            icons[2].GetComponent<Image>().color = new Vector4(icons[2].GetComponent<Image>().color.r, icons[2].GetComponent<Image>().color.g, icons[2].GetComponent<Image>().color.b, 100);
        }
        else
        {
            icons[2].GetComponent<Image>().color = new Vector4(icons[2].GetComponent<Image>().color.r, icons[2].GetComponent<Image>().color.g, icons[2].GetComponent<Image>().color.b, 0);
        }


        health.GetComponent<Text>().text = gameObject.GetComponent<NetworkPlayer>().health.ToString();
        score.GetComponent<Text>().text = gameObject.GetComponent<NetworkPlayer>().points.ToString();
        deaths.GetComponent<Text>().text = gameObject.GetComponent<NetworkPlayer>().deaths.ToString();
        
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

    }
}
