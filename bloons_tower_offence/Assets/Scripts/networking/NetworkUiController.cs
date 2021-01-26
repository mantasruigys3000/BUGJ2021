using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NetworkUiController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameslist;
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

        for(int c = 0; c < gameslist.transform.childCount; c++) {
            GameObject.Destroy(gameslist.transform.GetChild(c).gameObject);

        }

        currentGames = sm.currentGames;

        for (int i = 0; i < currentGames.Count; i++) {
            Debug.Log("creating button");
            GameObject button = Instantiate(listButtonPrefab, gameslist.transform);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(8.5f, 280 - (i * 150));


        }


        /*
        currentGames = sm.currentGames;

        gameslist.ClearOptions();
        List<string> options = new List<string>();

        for(int i = 0; i < currentGames.Count; i++) {
            string str = currentGames[i].game_name;
            options.Add(str);


        }

        gameslist.AddOptions(options);
        */






    }

    public void refreshClick() {
        sm.getGameList();
        StartCoroutine(nameof( setGames));


    }

    public void joinClick() {
        //sm.connectToPort = currentGames[gameslist.value].game_port;
        sm.connect();

    }

    public void hostClick() {
        sm.host();

    }
}
