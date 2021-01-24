using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;


public class CustomTelepathy : TelepathyTransport
{
    private void Awake() {
        if (SystemInfo.graphicsDeviceID == 0) {
            int p = int.Parse(Environment.GetCommandLineArgs()[1]);
            port = ushort.Parse(p.ToString());
        }


        Debug.Log("port should be" + port.ToString());
    }
}
