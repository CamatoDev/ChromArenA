# ChromArenA

> “Dans un monde où la perception définit la réalité, les couleurs ne sont pas qu’une simple teinte, mais une expression de la pensée et des valeurs. Chaque joueur appartient à une Tribu Chromatique, une faction qui croit en un idéal et lutte pour l’imposer en recouvrant le monde de sa couleur.”

**ChromArenA** est un TPS multijoueur à matchs courts, développé sous Unity 2022+ et C#.  
Les joueurs incarnent un Guerrier Chromatique dont l’arme est… la peinture ! Peignez le terrain, contrôlez des zones, survivez et éliminez vos adversaires dans des arènes modulaires.

---

## 🚀 Fonctionnalités clés

- **Terrain 100% modulaire**  
  Génération dynamique de maps variées à partir de tuiles Unity.
- **Mécanique de peinture**  
  Chaque tir « peint » le sol et les décors, changeant la couleur et les bonus associés.
- **Gameplay TPS complet**  
  Déplacement 360°, position de tir, accroupi, animations fluides.
- **Multijoueur avec Mirror**  
  Architecture client‑serveur hôte, synchronisation des joueurs et de l’état du terrain.
- **Prototype réseau local**  
  Test de la synchronisation des mouvements et de la peinture entre plusieurs clients.

---

## 📦 Prérequis

- **Unity** 2022.3 LTS (ou supérieure)  
- **Git** (pour cloner le dépôt)  
- **Visual Studio** ou tout autre IDE C# compatible  
- **Package Mirror** (installé via `Window > Package Manager` ou `Assets > Import Package > Custom Package`)

## 🔧 Installation

1. **Cloner le dépôt**  
   ```bash
   git clone https://github.com/MonSuperStudio/ChromArenA.git
   cd ChromArenA````

2. **Ouvrir dans Unity**

   * Lancez Unity Hub, ajoutez le dossier cloné comme projet existant.
   * Ouvrez la scène de démarrage `Assets/Scenes/Main.unity`.
3. **Installer Mirror**

   * Ouvrez **Window > Package Manager**.
   * Cliquez sur **+ > Add package from git URL…** et entrez :

     ```
     https://github.com/vis2k/Mirror.git#release
     ```
   * Attendez que le package soit importé.

---

## ▶️ Utilisation

* **Mode Éditeur**

  * Sélectionnez la scène `Assets/Scenes/Main.unity`.
  * Appuyez sur ▶️ pour lancer le jeu localement.
  * Dans l’onglet **Network Manager**, choisissez **Host** ou **Client** pour tester la synchronisation.

* **Build**

  * **File > Build Settings**
  * Ajoutez la scène `Main.unity` à la liste.
  * Sélectionnez **PC, Mac & Linux Standalone** et/ou **Android/iOS**.
  * Cliquez sur **Build and Run**.

---

## 🛣️ Roadmap

* [x] Terrain modulaire
* [x] Mouvement TPS & animations
* [x] Prototype de la peinture dynamique
* [x] Intégration Mirror & build réseau local
* [ ] Choix final du framework réseau (Mirror vs Photon)
* [ ] Passer en cloud / matchmaking
* [ ] Optimisations mobile
* [ ] Ajout des Tribu-Chromatiques et de leurs compétences
* [ ] UI / HUD final
* [ ] Tests et équilibrage

---

## 💬 Contribuer

1. Forkez ce dépôt.
2. Créez une branche `feature/MonFeature`.
3. Committez vos changements (`git commit -m "Ajout : ma super feature"`).
4. Poussez (`git push origin feature/MonFeature`).
5. Ouvrez une Pull Request.

Merci d’ouvrir une issue pour toute suggestion ou bug avant de coder ! 🙏

## 🤝 Remerciements

Un immense merci à mon collaborateur principal pour son soutien, ses idées et ses heures de debug acharnées ! 🎨🤖

## ⚖️ Licence

Ce projet est sous **MIT License**. Voir le fichier [LICENSE](LICENSE) pour plus de détails.
