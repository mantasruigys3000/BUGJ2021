using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    public int health = 100;
    [SyncVar]
    public int rayAmmo = 0;
    [SyncVar]
    public int nailAmmo = 0;
    [SyncVar]
    public int rocketAmmo = 0;
    [SyncVar]
    public int chosenWpn = 0;
    [SyncVar]
    public bool isDead = false;
    [SyncVar]
    public int points;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerIndex;
    [SyncVar]
    public bool hasCheese;


    public List<GameObject> icons;




    public GameObject rayGun;
    public GameObject nailGun;
    public GameObject rocketGun;
    public GameObject model;
    public GameObject cheesePrefab;
    public GameObject playerCheese;
    public GameObject rocket;


    [SyncVar]
    public Vector3 spawnPoint;




    void Start()
    {
        //spawnPoint = new Vector3(-10.6700001f, 2.66000009f, -22.1499996f);
        playerName = GetComponent<NetworkIdentity>().netId.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) {

            if(rayAmmo>0)
            {
                icons[0].GetComponent<Image>().color= new Vector4(icons[0].GetComponent<Image>().color.r, icons[0].GetComponent<Image>().color.g, icons[0].GetComponent<Image>().color.b, 100);
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

            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                CmdchangeWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                CmdchangeWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                CmdchangeWeapon(3);
            }

            if (rayAmmo > 0 && chosenWpn == 1) {
                rayGun.SetActive(true);
                nailGun.SetActive(false);
                rocketGun.SetActive(false);
            }

            if (nailAmmo > 0 && chosenWpn == 2) {
                rayGun.SetActive(false);
                nailGun.SetActive(true);
                rocketGun.SetActive(false);
            }

            if (rocketAmmo > 0 && chosenWpn == 3) {
                rayGun.SetActive(false);
                nailGun.SetActive(false);
                rocketGun.SetActive(true);
            }

            if (hasCheese) {
                playerCheese.SetActive(true);
            } else {
                playerCheese.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                CmdShootCheese();
            }
            if (Input.GetMouseButtonDown(0)) {
                if(chosenWpn == 3) {
                    CmdShootRocket();
                }
            }

        }
            

        if (isLocalPlayer) {
            if (health <= 0) {
                CmdDie();
            }
        }
        

    }
           
    [Command]
    public void CmdchangeWeapon(int weaponNr)
    {
        if (!isDead) {
            chosenWpn = weaponNr;
        }
        
    }
    
    //MUST BE CALLED FROM A COMMAND
    public void takeDamage(string _id = null,int dmg = 10) {
        health -= dmg;
        Debug.Log(gameObject.name + " " + health.ToString());
        if (health <= 0) {
            if (_id != null) {
                CustomNetworkManager.players[_id].addPoint();
            }
        }
        

    }

    [Command]
    public void CmdDie() {
        isDead = true;

        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<movementScript>().enabled = false;
        rayGun.SetActive(false);
        nailGun.SetActive(false);
        rocketGun.SetActive(false);
        model.SetActive(false);
        StartCoroutine(nameof(repsawnTimer));
        

        RpcDie();
    }

    [ClientRpc]
    public void RpcDie() {
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<movementScript>().enabled = false;
        rayGun.SetActive(false);
        nailGun.SetActive(false);
        rocketGun.SetActive(false);
        model.SetActive(false);
    }

    public IEnumerator repsawnTimer() {
        yield return new WaitForSeconds(3);
        isDead = false;
        health = 100;
        rayAmmo = 0;
        nailAmmo = 0;
        rocketAmmo = 0;
        chosenWpn = 0;


        gameObject.GetComponent<CharacterController>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        //gameObject.GetComponent<movementScript>().enabled = true;
        rayGun.SetActive(false);
        nailGun.SetActive(false);
        rocketGun.SetActive(false);
        model.SetActive(true);
        RpcRespawn();

    }

   
   
    [ClientRpc]
    public void RpcRespawn() {
        if (isLocalPlayer) {
            gameObject.GetComponent<movementScript>().enabled = true;
            gameObject.transform.position = spawnPoint;
        }
        gameObject.GetComponent<CharacterController>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        
        rayGun.SetActive(false);
        nailGun.SetActive(false);
        rocketGun.SetActive(false);
        model.SetActive(true);
    }

    
    public void addPoint() {
        points += 1;
        CustomNetworkManager.checkWin();

    }

    [Command]
    public void CmdShootCheese() {
        if (hasCheese) {
            hasCheese = false;
            playerCheese.SetActive(false);
            GameObject cheese = Instantiate(cheesePrefab,transform.position + new Vector3(0f,2f,0f) + (gameObject.transform.forward),Quaternion.identity);
            

            cheese.GetComponent<Rigidbody>().AddForce(gameObject.transform.Find("Camera").transform.forward * 10f, ForceMode.Impulse);
            NetworkServer.Spawn(cheese);

        }
        




    }

    [Command]
    public void CmdShootRocket() {
        if(rocketAmmo > 0) {
            GameObject _rocket = Instantiate(
                rocket,
                rocketGun.transform.Find("gunEnd").transform.position,
                gameObject.transform.Find("Camera").transform.rotation
            );

            _rocket.transform.Rotate(90f, 0, 0);
            //_rocket.transform.Translate(0, 0, 2f);

            _rocket.GetComponent<Rigidbody>().AddForce(_rocket.transform.up *  30f, ForceMode.Impulse);
            _rocket.GetComponent<rocketScript>().shotFrom = gameObject.GetComponent<NetworkIdentity>().netId.ToString();

            
            NetworkServer.Spawn(_rocket);



        }
    }
    [ClientRpc]
    public void Rpcexplode(Vector3 pos) {
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(
                20f,
                pos,
                20f,
                50f,
                ForceMode.Impulse
                );
    }


}
