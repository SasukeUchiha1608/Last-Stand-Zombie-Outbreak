# 🧟 Last Stand: Zombie Outbreak

**Last Stand: Zombie Outbreak** is a 2D top-down survival shooter developed as a capstone project. The player must survive waves of zombies, manage limited ammo, and aim accurately using twin-stick style shooting mechanics. Featuring procedural enemy pathfinding, pickups, screen shake effects, and a reloading system, the game offers intense moment-to-moment decision-making.

---

## 🎮 Gameplay Overview

- **Genre**: Top-Down Survival Shooter
- **Platform**: PC (Windows)
- **Engine**: Unity 6
- **Goal**: Survive against endless waves of zombies while collecting pickups and managing your resources.
- **Playtime**: Each session is designed to last 5+ minutes with an eventual win/loss outcome.

---

## 🧩 Features

- 🔫 Twin-stick aiming and shooting with mouse control.
- 🧠 Enemies use A* pathfinding for intelligent movement.
- 💀 Persistent dead bodies provide visual battlefield feedback.
- 🧨 Ammo management system with reloading mechanic.
- 🌊 Wave-based progression with increasing difficulty.
- 💥 Muzzle flash, screen shake, and audio effects for immersion.
- ⏸️ Pause system with clearly indicated game states.
- 📺 Resolution and fullscreen toggle options that persist between sessions.
- 🎵 Background music and SFX with volume controls.

---

## 🧪 Playtesting Summary

### 🧷 Test #1 – Bullet Direction Fix
Initial builds had bullets shooting in random directions regardless of aim. This was resolved by restructuring the player hierarchy so that the FirePoint rotated along with the player's graphics transform. Verified after several tests.

### 🧷 Test #2 – Sprite Layering and Prefab Rendering
Enemies and environment prefabs were visually stacking incorrectly. Dead zombies were spawning under the ground. We resolved this by adjusting the sprite rendering layers and sorting orders of all relevant objects.

### 🧷 Test #3 – Fullscreen and Resolution Persistence
Settings like fullscreen and resolution were not saving across sessions. We fixed this by implementing persistent PlayerPrefs and retesting via build. Confirmed working on multiple launches.

---

## 📖 Reflection

Throughout development, I iteratively improved gameplay based on testing and feedback. Initially, player aiming and shooting did not work correctly, and some UI elements were missing. By incrementally fixing issues—like aiming mechanics, resolution settings, and object rendering—I learned the importance of small, testable changes. I also gained experience with Unity’s UI system, persistent settings, and polish features like audio balancing and game feel. This project taught me how to troubleshoot issues systematically and deliver a playable experience by deadline.

---

## 📢 Credits

This game was developed by **Gustavo Alberto Lopez Moreno** as a capstone project.

### Audio
- **Gunshot Sound Effect** by [freesound_community](https://pixabay.com/users/freesound_community-46691455/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=7152) from [Pixabay](https://pixabay.com/sound-effects//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=7152)
- **"Horror Box" Music** by [epic_musictracks](https://pixabay.com/users/epic_musictracks-20267811/) from Pixabay
- **"They Are Coming (Halloween Zombie March)"** by [pixabay](https://pixabay.com)

### Art & Visual Assets
- **Topdown (Shooter) Pack**  
  by Kenney Vleugels ([Kenney.nl](https://kenney.nl))  
  License: [Creative Commons Zero (CC0)](http://creativecommons.org/publicdomain/zero/1.0/)  
  > *You may use these assets in personal and commercial projects. Credit (Kenney or www.kenney.nl) would be nice but is not mandatory.*  
  Support Kenney: [support.kenney.nl](http://support.kenney.nl) | Requests: [request.kenney.nl](http://request.kenney.nl) | Twitter: [@KenneyWings](https://twitter.com/KenneyWings)

- **2D Fantasy Character Pack Demo**  
  Characters by Fabricio Correia Batista

- **Warped Shooting FX**  
  by Ansimuz

### Pathfinding
- **A* Pathfinding Project (Free Version)**  
  by [Aron Granberg](https://arongranberg.com/astar)  
  > Provides fast and flexible pathfinding support for Unity.

---

## 📂 How to Play

1. Download and unzip the game build.
2. Launch the executable file.
3. Use **WASD** to move, and **mouse** to aim and shoot.
4. Press **R** to reload, and **Esc** to pause.
5. Try to survive as long as possible!

---

## 💡 License

This project uses third-party assets that are under CC0 or public attribution licenses. Please refer to the Credits section above for full licensing details.

---

## 🚀 Future Improvements (If Continued)

- Add multiple weapon types and upgrade system.
- Implement difficulty scaling over time.
- Introduce UI health bar for zombies.
- Add controller support and sound settings submenu.
