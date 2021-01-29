using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;




public class CustomNetworkManager : NetworkManager {


    public ServerManager sm;
    public static Dictionary<string, NetworkPlayer> players;
    public List<int> takenSides;
    public List<Vector3> spawns;
    






    public override void Awake() {
        base.Awake();
        sm = GetComponent<ServerManager>();

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
            //sm.connectGameServerToMaster();
        }

        players = new Dictionary<string, NetworkPlayer>();
    }

    public override void OnServerSceneChanged(string sceneName) {
        base.OnServerSceneChanged(sceneName);
        spawns = GameObject.Find("Mapinfo").GetComponent<mapInfo>().spawns;
    }

    public static NetworkPlayer getPlayer(string _id) {
        return players[_id];
    }

    public static void checkWin(){
       
        foreach(KeyValuePair<string,NetworkPlayer> kv in players) {
            if(kv.Value.points >= 3) {
                Debug.Log(kv.Value.playerName + " wins");
            }
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn) {
        base.OnServerDisconnect(conn);
        players.Remove(conn.identity.netId.ToString());
    }





}
