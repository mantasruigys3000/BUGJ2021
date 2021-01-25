using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;




public class CustomNetworkManager : NetworkManager
{


    public ServerManager sm;
    public static  Dictionary<string, NetworkPlayer> players;


   



    public override void Awake() {
        base.Awake();
        sm = GetComponent<ServerManager>();

    }
    public override void OnServerAddPlayer(NetworkConnection conn) {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        NetworkServer.AddPlayerForConnection(conn, player);
        players.Add(conn.identity.netId.ToString(),player.GetComponent<NetworkPlayer>());
        Debug.Log("Added player " + conn.identity.netId.ToString());
        //Debug.Log(getPlayer(conn.identity.netId.ToString()));



    }

    

    public override void OnStartServer() {
        base.OnStartServer();
        sm.connectGameServerToMaster();
        players = new Dictionary<string, NetworkPlayer>();




        //Connect to main server

    }

    public static NetworkPlayer getPlayer(string _id) {
        return players[_id];


    }

   



}
