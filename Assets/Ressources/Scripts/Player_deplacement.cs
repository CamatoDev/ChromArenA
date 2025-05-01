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
        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            characterController.Move(velocity * Time.deltaTime);
        }


        //Vérification du déplacement 
        if (moveAmount > 0) 
        {
            //Orientation du joueur
            targetRotation = Quaternion.LookRotation(moveDirection);
            moveSpeed = 5f;
        }

        //Pour courir
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveAmount = 2f;
            moveSpeed = runSpeed;
        }

        //Gestion de la rotation smooth
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);




        // Gestion des animaions avec armes
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        // Vérification des valeurs des inputs
        float shootState = 0f;

        if (horizontalValue > 0)
        {
            shootState = 1f;
        }
        if (horizontalValue < 0)
        {
            shootState = -1f;
        }
        if (verticalValue > 0)
        {
            shootState = 2f;
        }
        if (verticalValue < 0)
        {
            shootState = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("RightClick", true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("RightClick", false);
        }

        animator.SetFloat("ShootState", shootState, 0.2f, Time.deltaTime);
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
}
