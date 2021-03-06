using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;




public class CustomNetworkManager : NetworkManager {


    public ServerManager sm;
    public static Dictionary<string, NetworkPlayer> players;
    public List<int> takenSides;
    public List<Vector3> spawns;
    public static Vector3 cheeseSpawn;
    public GameObject _cheesePrefab;
    public static GameObject cheesePrefab;
    public bool resartFlag = false;

    public List<Material> mats;
    







    public override void Start() {
        base.Start();
        

    }

    public override void Awake() {
        base.Awake();
        sm = GetComponent<ServerManager>();
        cheesePrefab = _cheesePrefab;


    }
    public override void OnServerAddPlayer(NetworkConnection conn) {

        int index = 0;
        for (index = 0; index < 4; index++) {
            if (!takenSides.Contains(index)) {
                takenSides.Add(index);
                Debug.Log("assigned index " + index.ToString());
                break;
            }
        }

        Transform startPos = GetStartPosition();
    
        GameObject player = Instantiate(playerPrefab, spawns[index], Quaternion.identity); ;
        //player.
       


        NetworkServer.AddPlayerForConnection(conn, player);
        players.Add(conn.identity.netId.ToString(), player.GetComponent<NetworkPlayer>());
        players[conn.identity.netId.ToString()].spawnPoint = spawns[index];
        players[conn.identity.netId.ToString()].playerIndex = index;



        Debug.Log("Added player " + conn.identity.netId.ToString());
        Debug.Log("spawn is " + spawns[index]);
        //Debug.Log(players[conn.identity.netId.ToString()].playerName);
        //Debug.Log(getPlayer(conn.identity.netId.ToString()));



    }



    public override void OnStartServer() {
        base.OnStartServer();
        
        if (sm != null) {
            //UNCOMMENT ON DEPLOYMENT
            sm.connectGameServerToMaster();
        }

        players = new Dictionary<string, NetworkPlayer>();
    }

    public override void OnServerSceneChanged(string sceneName) {
        base.OnServerSceneChanged(sceneName);
        for(int s = 0; s < GameObject.Find("Mapinfo").GetComponent<mapInfo>().spawns.Count; s++) {
            spawns.Add(GameObject.Find("Mapinfo").GetComponent<mapInfo>().spawns[s].position);
        }
        cheeseSpawn = GameObject.Find("Mapinfo").GetComponent<mapInfo>().cheeseSpawn.position;


    }

    public static NetworkPlayer getPlayer(string _id) {
        return players[_id];
    }

    public static void checkWin(){

        
       
        foreach(KeyValuePair<string,NetworkPlayer> kv in players) {
            if(kv.Value.points >= 20) {
                string txt = kv.Value.playerName + " wins";
                kv.Value.winsText.text = txt;
                foreach (KeyValuePair<string, NetworkPlayer> kvv in players) {

                    if (kvv.Value.isServer) {
                        kvv.Value.RpcSetWinsText(txt);

                    }
                }



                    

                    kv.Value.RpcSetWinsText(txt);
                //restartGame();
                GameObject.Find("GameManager").GetComponent<GameManager>().restartGame();
                 


            }
        }
    }

    public static void restartGame() {
       
    }

    public static IEnumerator restart() {
        yield return new WaitForSeconds(7);
        foreach (KeyValuePair<string, NetworkPlayer> kv in players) {
            kv.Value.isDead = false;
            kv.Value.health = 100;
            kv.Value.rayAmmo = 0;
            kv.Value.nailAmmo = 0;
            kv.Value.rocketAmmo = 0;
            kv.Value.chosenWpn = 0;


            kv.Value.gameObject.GetComponent<CharacterController>().enabled = true;
            kv.Value.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            //gameObject.GetComponent<movementScript>().enabled = true;
            kv.Value.rayGun.SetActive(false);
            kv.Value.nailGun.SetActive(false);
            kv.Value.rocketGun.SetActive(false);
            kv.Value.model.SetActive(true);
            kv.Value.RpcRespawn();
        }

    }

   

    public override void OnServerDisconnect(NetworkConnection conn) {
        takenSides.Remove(players[conn.identity.netId.ToString()].playerIndex);
        players.Remove(conn.identity.netId.ToString());
        base.OnServerDisconnect(conn);
        
    }

    
    public static void SpawnCheese() {
        GameObject cheese = Instantiate(cheesePrefab,cheeseSpawn,Quaternion.identity);
        NetworkServer.Spawn(cheese);


    }





}
