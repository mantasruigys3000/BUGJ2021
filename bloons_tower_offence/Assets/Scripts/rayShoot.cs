using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayShoot : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;
    // Start is called before the first frame update

    public GameObject bulletPrefab;
    public GameObject gunEnd;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit);
           
        }
        var bullet = Instantiate(bulletPrefab, gunEnd.transform.position, gunEnd.transform.rotation);
        bullet.transform.LookAt(cam.transform.forward);
        bullet.GetComponent<Rigidbody>().velocity = (hit.point - gunEnd.transform.position).normalized * 20;
    }
}
