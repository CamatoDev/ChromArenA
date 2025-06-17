using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using System.Collections;

public class Player : NetworkBehaviour
{
    // Variable qui verifie si le joueur est mort 
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }


    [Header("Player lives")]
    [SerializeField]
    private float maxHealt = 100f;
    [SyncVar]
    private float currentHealth;

    // Array contenant les component à désactiver à la mort du joueur local 
    [SerializeField]
    private Behaviour[] disableOnDeath;
    // Array contenant le component qui était déjà désactivé au départ
    private bool[] wasEnabledOnStart;

    // La fonction du lancement des paramètre de base 
    public void Setup()
    {
        wasEnabledOnStart = new bool[disableOnDeath.Length];
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            wasEnabledOnStart[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            RpcTakeDamage(200);
        }
    }

    // Couroutine pour le respaw 
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.macthSettings.respawnTime);
        // Après l'élimination du joueur et la fin du délais de réaparition
        SetDefaults();

        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

    // Valeur par défaut des paramètre du joueur
    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealt;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabledOnStart[i];
        }

        // Récuperation du collider du joueur 
        Collider playerCollider = GetComponent<Collider>();
        if(playerCollider != null)
        {
            // On active le collider (après la mort le coliider doit être réactivé)
            playerCollider.enabled = true;
        }
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(transform.name + " à maintenant " + currentHealth + " points de vie.");

        //Si le joueur n'as plus de point de vie il meurt
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    // Fonction pour la mort du personnage 
    public void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        // Récuperation du collider du joueur 
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
        {
            // On désactive le collider (à la mort le coliider doit être désactivé)
            playerCollider.enabled = false;
        }

        Debug.Log(transform.name + " a été éliminé.");

        // Lacement du respawn
        StartCoroutine(Respawn());
    }
}
