using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_mouvement : MonoBehaviour
{
    public Transform james;
    [Header("Deplacement Speeds")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float aimingSpeed = 2f;
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
    public bool isCrouch = false;

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
        //if (!Input.GetKey(KeyCode.Mouse0))
        //{
        //    characterController.Move(velocity * Time.deltaTime);
        //}


        //Vérification du déplacements
        if (moveAmount > 0 && !Input.GetKey(KeyCode.Mouse1) && !isCrouch) 
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
        //james.rotation = Quaternion.RotateTowards(james.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if(!isCrouch)
            animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);

        // Gestion des animaions avec armes
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        // Vérification des valeurs des inputs pour le déplacement en position accroupi
        float crouchState = 0f;
        if (isCrouch)
        {
            if (horizontalValue > 0)
            {
                crouchState = 1f;
            }
            if (horizontalValue < 0)
            {
                crouchState = -1f;
            }
            if (verticalValue > 0)
            {
                crouchState = 2f;
            }
            if (verticalValue < 0)
            {
                crouchState = -2f;
            }
            if (moveAmount <= 0f)
                crouchState = 0.1f;

            animator.SetFloat("crouchState", crouchState, 0.2f, Time.deltaTime);
        }

        // Vérification des valeurs des inputs pour le déplacement en position de tir
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
        if (moveAmount <= 0f)
            shootState = 0.1f;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("RightClick", true);
            moveSpeed = aimingSpeed;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("RightClick", false);
        }

        if (Input.GetKeyDown(KeyCode.C) && !isCrouch)
        {
            Debug.Log("accroupi");
            animator.SetTrigger("Crouch");
            moveSpeed = aimingSpeed;
            isCrouch = !isCrouch;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isCrouch)
        {
            Debug.Log("Debout");
            animator.SetTrigger("standUp");
            moveSpeed = 5f;
            isCrouch = !isCrouch;
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
