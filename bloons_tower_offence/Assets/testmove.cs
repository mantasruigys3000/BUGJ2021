using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class testmove : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                transform.Translate(0, 1, 0);

            }
        }
    }
}
