using UnityEngine;
using Mirror;

public class Player_shoot : NetworkBehaviour
{
    private Player_mouvement _playerMove;
    public Transform playerCamera;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRange = 10f;
    private Renderer _rend;
    private Animator _animator;

    public float fireRate = 1f;
    private float _fireCountDown = 0f;

    [SerializeField]
    private LayerMask _mask;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _playerMove = gameObject.GetComponent<Player_mouvement>();
        //playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = playerCamera.rotation;
        rotation.z = 0;
        rotation.x = 0;
        firePoint.rotation = playerCamera.rotation;
        transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _fireCountDown <= 0 && Cursor.lockState == CursorLockMode.Locked)
        {
            _playerMove.isCrouch = !_playerMove.isCrouch;
            Shoot();
            _fireCountDown = 1 / fireRate;
        }

        _fireCountDown -= Time.deltaTime;
    }

    [Client]
    public void Shoot()
    {
        Debug.Log("Tir effectué !");
        _animator.SetTrigger("LeftClick");
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        _rend = bullet.GetComponent<Renderer>();
        RaycastHit hit;

        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.TransformDirection(Vector3.forward), out hit, fireRange, _mask))
        {
            Debug.DrawLine(firePoint.transform.position, hit.point, Color.red);

            if (hit.transform.tag == "Obstacle")
            {
                CmdPlayerShot(hit.transform.name);
                //Destroy(bullet);
                hit.transform.GetComponent<Renderer>().material.color = _rend.material.color;
            }
            if (hit.transform.tag == "Player")
            {
                CmdPlayerShot(hit.transform.name);
            }
        }
    }

    [Command]
    public void CmdPlayerShot(string playerName)
    {
        Debug.Log(playerName + " à été touché");
    }
}
