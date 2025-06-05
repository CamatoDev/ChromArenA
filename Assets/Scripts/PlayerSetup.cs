using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    // Liste des composants a desactives
    [SerializeField]
    Behaviour[] componentsToDisable;

    // La camera de la scene par défaut
    Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            // Boucle de desactivation des composants qui ne concerne pas directement notre instance de joueur 
            //(Pour éviter qu'un Joueuer controle tout les autres)
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if(sceneCamera != null)
        {
        sceneCamera.gameObject.SetActive(true);
        }
    }
}
