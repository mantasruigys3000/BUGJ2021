using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NameGetter : MonoBehaviour
{
    public static string playerName;
    public InputField getname;


   

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "TeoMenu") {
            playerName = getname.text;

        }
    }
}
