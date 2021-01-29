using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiSelection : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
          /*  for(int i=0; i<uiElements.Count; i++)
            {
                displayIcons(uiElements[i].gameObject);
            }
          */
        }
    }

    void displayIcons(GameObject icon)
    {
        icon.GetComponent<Image>().color = new Vector4(icon.GetComponent<Image>().color.r, icon.GetComponent<Image>().color.g, icon.GetComponent<Image>().color.b, Mathf.Lerp(0, 255, 0.2f * Time.deltaTime));
    }
}
