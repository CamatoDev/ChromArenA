using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_stats : MonoBehaviour
{
    // Rajouter des variable relative au joueur (vie, chargeur, consomable, bonus...)
    [Header("Player lives")]
    public float currentLive;
    // Barre de vie 
    public Image healthBar;
    public float damage;
    public bool isDead;
    private float startLive = 100f;

    // Start is called before the first frame update
    void Start()
    {
        currentLive = startLive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
