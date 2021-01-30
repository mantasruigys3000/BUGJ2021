using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class audioSync : NetworkBehaviour
{
    private AudioSource source;
    public List<AudioClip> clips;
    void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playSound(int id)
    {
        if(id >=0 && id < clips.Count)
        {
            CmdSendServerSoundID(id);
        }   
    }

    [Command]
    void CmdSendServerSoundID(int id)
    {
        RpcSendSoundID(id);
    }

    [ClientRpc]
    void RpcSendSoundID(int id)
    {
        Debug.Log("sTEP4");
        source.PlayOneShot(clips[id]);
    }
}
