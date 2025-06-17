using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [Header("Player lives")]
    [SerializeField]
    private float maxHealt = 100f;
    [SyncVar]
    private float currentHealth;

    private void Awake()
    {
        SetDefault();
    }

    public void SetDefault()
    {
        currentHealth = maxHealt;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(transform.name + " à maintenant " + currentHealth + " points de vie.");
    }
}
