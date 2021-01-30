using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class rayScript : NetworkBehaviour {
    // Start is called before the first frame update

    public GameObject ray;
    public ParticleSystem trail;
    

    void Start() {
        //ray.GetComponent<MeshRenderer>().enabled = false;
        trail.GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Impulse);

    }
}

