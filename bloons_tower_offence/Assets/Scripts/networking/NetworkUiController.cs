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
    public InputField name;
    public Text name_error;
    int buttonClicked;


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

        List<Button> buttons = new List<Button>();
        

        for (int i = 0; i < currentGames.Count; i++) {
            ServerManager.gameInfo game = currentGames[i];
            Debug.Log("creating button");
            GameObject button = Instantiate(listButtonPrefab, gameslist.transform);
            ListButton lstbtn = button.GetComponent<ListButton>();
            lstbtn.lobby_name.text = game.game_name + "'s game";
            //lstbtn.map_name.text = "Dust 2";

            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(8.5f, 280 - (i * 150));
            lstbtn.index = i;
            buttons.Add(button.GetComponent<Button>());






        }

        //for each button
        for(int b = 0; b < buttons.Count; b++) {
            int temp = b;
            buttons[b].onClick.AddListener(() => { buttonClicked = temp; }) ;

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
        sm.connectToPort = currentGames[buttonClicked].game_port;
        sm.connect();

    }

    public void hostClick() {
        if(name.text == "") {
            name_error.enabled = true;
            return;

        }
        sm.host(name.text);

    }

    public void pressListButton() {
        Debug.Log(this);

    }
}
