using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NetworkUiController : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown gameslist;
    public Button refresh;
    public Button host;
    public Button join;
    public ServerManager sm;

    public GameObject listButtonPrefab;



    public string currentAddr = "";
    public ushort currentPort = 0;

    public List<ServerManager.gameInfo> currentGames;


    private void Start() {
        if(sm == null) {
            Debug.LogError("UI CONTROLLER NEEDS A SERVERMANAGER");
            return;
        }
        refresh.onClick.AddListener(refreshClick);
        join.onClick.AddListener(joinClick);
        host.onClick.AddListener(hostClick);
    }
    // Update is called once per frame
    void Update()
    {

        
      
    }

    public IEnumerator setGames() {
        yield return new WaitForSeconds(2);
        currentGames = sm.currentGames;

        gameslist.ClearOptions();
        List<string> options = new List<string>();

        for(int i = 0; i < currentGames.Count; i++) {
            string str = currentGames[i].game_name;
            options.Add(str);


        }

        gameslist.AddOptions(options);



    }

    public void refreshClick() {
        sm.getGameList();
        StartCoroutine(nameof( setGames));


    }

    public void joinClick() {
        sm.connectToPort = currentGames[gameslist.value].game_port;
        sm.connect();

    }

    public void hostClick() {
        sm.host();

    }
}
