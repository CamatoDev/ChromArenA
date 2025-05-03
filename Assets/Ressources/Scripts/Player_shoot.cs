using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shoot : MonoBehaviour
{
    private Player_mouvement _playerMove;
    public Transform camera;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRange = 10f;
    private Renderer _rend;
    private Animator _animator;

    public float fireRate = 1f;
    private float _fireCountDown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _playerMove = gameObject.GetComponent<Player_mouvement>();
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = camera.rotation;
        rotation.z = 0;
        rotation.x = 0;
        firePoint.rotation = camera.rotation;
        transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _fireCountDown <= 0)
        {
            Shoot();
            _fireCountDown = 1 / fireRate;
        }

        _fireCountDown -= Time.deltaTime;
    }

    public void Shoot()
    {
        Debug.Log("Tir effectué !");
        _animator.SetTrigger("LeftClick");
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        _rend = bullet.GetComponent<Renderer>();
        RaycastHit hit;

        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.TransformDirection(Vector3.forward), out hit, fireRange))
        {
            Debug.DrawLine(firePoint.transform.position, hit.point, Color.red);

            if (hit.transform.tag == "Obstacle")
            {
                //Destroy(bullet);
                hit.transform.GetComponent<Renderer>().material.color = _rend.material.color;
            }
        }
    }
}
