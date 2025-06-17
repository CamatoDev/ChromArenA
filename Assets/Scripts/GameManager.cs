using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public MacthSettings macthSettings;

    // Création d'un singleton
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }

        Debug.LogError("Plus d'une instance de GameManager dans la scène.");
    }

    // Méthode pour enregistrer un joueur (lors de son instantiation)
    public static void RegisterPlayer(string netID, Player player)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, player);
        // Modification pour que le nom de l'objet et ça reférence dans le dico soit la même
        player.transform.name = playerId;
    }

    // Méthode pour retirer un joueur du dico (si il ce deconnecte par exemple)
    public static void UnRegisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static Player GetPlayer(string playerId)
    {
        return players[playerId];
    }
}
