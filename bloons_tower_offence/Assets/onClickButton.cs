using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onClickButton : MonoBehaviour
{
    public Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(toggleBtn);

    }

    // Update is called once per frame
    void toggleBtn()
    {
        GetComponentInChildren<Toggle>().isOn = !GetComponentInChildren<Toggle>().isOn;
    }
}
