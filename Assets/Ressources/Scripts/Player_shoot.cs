using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRange = 10f;
    private Renderer rend;

    public float fireRate = 1f;
    private float fireCountDown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    public void Shoot()
    {
        Debug.Log("Tir effectué !");
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        rend = bullet.GetComponent<Renderer>();
        RaycastHit hit;

        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.TransformDirection(Vector3.forward), out hit, fireRange))
        {
            Debug.DrawLine(firePoint.transform.position, hit.point, Color.red);

            if (hit.transform.tag == "Obstacle")
            {
                //Destroy(bullet);
                hit.transform.GetComponent<Renderer>().material.color = rend.material.color;
            }
        }
    }
}
