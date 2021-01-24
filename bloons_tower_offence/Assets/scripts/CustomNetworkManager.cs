using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class CustomNetworkManager : NetworkManager
{
    public class Match {
        public System.Guid matchId;

        public Match(System.Guid _id) {
            this.matchId = _id;

        }
    }

    public List<Match> matches;


    public override void OnStartHost() {
        matches = new List<Match>();
        Match firstMatch = new Match(System.Guid.NewGuid());
        matches.Add(firstMatch);



        base.OnStartHost();
    }

    public override void OnServerAddPlayer(NetworkConnection conn) {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);
        //player.GetComponent<NetworkMatchChecker>().matchId = matches[0].matchId;

        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
