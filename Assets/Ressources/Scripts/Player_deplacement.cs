using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_mouvement : MonoBehaviour
{
    [Header("Deplacement Speeds")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float rotationSpeed = 500;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;
    bool isGrounded;
    float ySpeed;

    [Header("Player info")]
    public float playerScore = 0f;
    public float playerLive = 100f;
    bool haveKey = false;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;
    Quaternion targetRotation;

    void Awake()
    {
        //Récupération du component de la camera principal lors du lancement 
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Verification des valeurs des Input  
        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        //Valeur du déplacement 
        Vector3 moveInput = new Vector3(horizontal, 0, vertical).normalized;

        //Orientaton du joueur 
        var moveDirection = cameraController.PlanarRotation * moveInput;

        GroundeCheck();
        Debug.Log("isGround = " + isGrounded);
        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        //Velocité du deplacement 
        var velocity = moveDirection * moveSpeed;
        //on fait tomber le joueur 
        velocity.y = ySpeed;
        //Déplacement du joueur
        characterController.Move(velocity * Time.deltaTime);


        //Vérification du déplacement 
        if (moveAmount > 0) 
        {
            //Orientation du joueur
            targetRotation = Quaternion.LookRotation(moveDirection);
            moveSpeed = 5f;
        }
        
        //Pour courir
        //if(Input.GetKey(KeyCode.LeftShift))
        //{
        //    moveAmount = 2f;
        //    moveSpeed = runSpeed;
        //}

        //Gestion de la rotation smooth
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }

    void GroundeCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Pick"))
        {
            Debug.Log("Le joueur à touché un piège.");
            Debug.Log("- 5 points de vie!");
            playerLive -= 5f;
        }

        if (collision.transform.CompareTag("DoorClose"))
        {
            if (haveKey)
            {
                Debug.Log("Le joueur a une clé pour ouvir la porte.");
                Destroy(collision.transform.gameObject);
                Debug.Log("La porte s'ouvre !");
                haveKey = false;
            }
            else
            {
                Debug.Log("Le joueur n'a de clé pour ouvir la porte.");
                Debug.Log("Allez chercher la clé !");
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Pick"))
        {
            Debug.Log("- 1 points de vie!");
            playerLive -= 1f;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Pick"))
        {
            Debug.Log("exit to collision");
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("coinSilver"))
        {
            Debug.Log("Le joueur à touché la pièce d'argent.");
            Destroy(collision.transform.gameObject);
            Debug.Log("+ 10 points !");
            playerScore += 10f;
        }

        if (collision.transform.CompareTag("coinGold"))
        {
            Debug.Log("Le joueur à touché la pièce d'or.");
            Destroy(collision.transform.gameObject);
            Debug.Log("+ 20 points !");
            playerScore += 20f;
        }

        if (collision.transform.CompareTag("BigCoinGold"))
        {
            Debug.Log("Le joueur à touché la pièce d'or géante.");
            Destroy(collision.transform.gameObject);
            Debug.Log("+ 100 points !");
            playerScore += 100f;
        }

        if (collision.transform.CompareTag("Key"))
        {
            Debug.Log("Le joueur à récuperer une clé.");
            Destroy(collision.transform.gameObject);
            haveKey = true;
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.transform.CompareTag("coinSilver") ||
            collision.transform.CompareTag("coinGold"))
        {
            Debug.Log("stay in Trigger");
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("coinSilver") ||
            collision.transform.CompareTag("coinGold"))
        {
            Debug.Log("exit to Trigger");
        }
    }
}
