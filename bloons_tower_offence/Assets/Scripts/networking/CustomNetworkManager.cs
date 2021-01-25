using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;




public class CustomNetworkManager : NetworkManager
{


    public ServerManager sm;
   



    public override void Awake() {
        base.Awake();
        sm = GetComponent<ServerManager>();

    }
    public override void OnServerAddPlayer(NetworkConnection conn) {
        base.OnServerAddPlayer(conn);

    }

    


    public override void OnStartServer() {
        base.OnStartServer();
        sm.connectGameServerToMaster();


        //Connect to main server

    }

   



}
