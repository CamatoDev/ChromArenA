using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    // Liste des composants a desactives
    [SerializeField]
    Behaviour[] componentsToDisable;

    // La camera de la scene par défaut
    Camera sceneCamera;

    [SerializeField]
    private string _remoteLayerName = "RemotePlayer";

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemotePlayer();
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

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        // Enregister le joueur
        GameManager.RegisterPlayer(netID, player);
    }

    private void AssignRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(_remoteLayerName);
    }

    private void DisableComponents()
    {
        // Boucle de desactivation des composants qui ne concerne pas directement notre instance de joueur 
        //(Pour éviter qu'un Joueuer controle tout les autres)
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        if(sceneCamera != null)
        {
        sceneCamera.gameObject.SetActive(true);
        }

        // Desenregistrer le joueur dès que désactivé
        GameManager.UnRegisterPlayer(transform.name);
    }
}
