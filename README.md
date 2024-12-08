# Star Wars - Rough Path - The Game

## Table of Contents
1. [Introduction](#introduction)
2. [Scenes](#scenes)
3. [Maze and Level Data](#maze-and-level-data)
4. [Asset Access Documentation](#asset-access-documentation)
5. [Important Files and Folders](#important-files-and-folders)
6. [Testing](#testing)
7. [Conclusion](#conclusion)

---

## Introduction

Welcome to **Star Wars - Rough Path - The Game**, a pixel-art journey through the galaxy far, far away! This 2D space adventure challenges you to navigate treacherous asteroid fields, outsmart Imperial forces, and defeat iconic Star Wars villains in epic boss fights. Featuring a detailed menu system, complete with adjustable music settings, a "How to Play" guide, and a New Game / Load Game checkpoint system, the game offers an immersive retro experience.

### The Adventure Awaits:
- **Level 1:** Navigate an asteroid field and prepare for a thrilling boss fight against Darth Vader’s TIE fighter.
- **Level 2:** Engage in dogfights with swarms of smaller TIE fighters before facing the intimidating Star Destroyer.
- **Level 3:** Survive a combination of asteroids and enemy ships before taking on the ultimate challenge: the Death Star itself.

Will you harness the power of the Force to bring peace to the galaxy, or will you succumb to the overwhelming might of the Empire? The choice is yours—just make sure to dodge those asteroids!

---

## Scenes

The game's content is structured into Unity scenes, all located in the `Assets/Scenes` folder. These scenes bring the galaxy to life and provide a seamless narrative and gameplay experience.

### Available Scenes

- **Main Menu:**
  - `Main Menu.unity`: The central hub where you can start a new game, load a checkpoint, view the credits, or adjust game settings.

- **Levels:**
  - `Level 1.unity`: The first level, featuring an asteroid field and Darth Vader's TIE fighter.
  - `Level 2.unity`: The second level, with swarms of TIE fighters and the Star Destroyer.
  - `Level 3.unity`: The climactic level, combining all challenges and culminating in a boss fight against the Death Star.

- **Cutscenes:**
  - `Level1Cutscene.unity`, `Level2Cutscene.unity`, `Level3Cutscene.unity`: Story-driven scenes that connect the gameplay levels.

- **Intro Scene:**
  - `IntroScene.unity`: A cinematic introduction with music and animation, presenting the game's backstory. For those eager to jump into the action, the cutscene is skippable using the "Skip" button.

---

## Maze and Level Data

### Level Data

The game's level information is stored in `.asset` files, located in the `Assets/LevelData` folder. Each file provides the following minimal information:
- **Level Name:** The name of the current level.
- **Next Level Cutscene:** The cutscene to be played after completing the level.

#### Level Data Files
- `Level1Data.asset`: Data for Level 1.
- `Level2Data.asset`: Data for Level 2.
- `Level3Data.asset`: Data for Level 3.

These files are lightweight and used to connect gameplay levels with their respective cutscenes.

---

## Asset Access Documentation

### Backgrounds
- **Location:** `Assets/Backgrounds`
- **Key Files:**
  - `Level1_background.jpg`: Background for Level 1.
  - `Level2_background.jpg`: Background for Level 2.
  - `Level3_background.jpg`: Background for Level 3.

### Enemies
- **Location:** `Assets/Enemies`
- **Key Files:**
  - `asteroid.png`: Asteroid sprite.
  - `boss_1_darth_tie.png`: Darth Vader's TIE fighter sprite.
  - `Star_destroyer.png`: Star Destroyer sprite.
  - `Death_star.png`: The mighty Death Star sprite.

### Fonts
- **Location:** `Assets/Fonts`
- **Key Files:**
  - `BPdotsSquare.otf`: Retro-style font used in menus and UI.
  - `Starjedi SDF.asset`: Star Wars-themed font for titles.

### Music
- **Location:** `Assets/Music`
- **Key Files:**
  - `Imperial Attack.mp3`: The action-packed Imperial Attack theme.
  - `The Imperial March.mp3`: The iconic theme of Darth Vader.
  - `Main Title_Rebel Blockade Runne.mp3`: The classic Star Wars opening crawl music.

### Players
- **Location:** `Assets/Players`
- **Key Files:**
  - `x-wing1.png`: The sprite for the player's X-Wing.

---

## Important Files and Folders

### Game Scripts
- **Location:** `Assets/Scripts`
- **Key Files:**
  - `GameManager.cs`: Manages the game's core functionality.
  - `GameManager.cs`: Handles the complete menu system, allowing the player to start a new game, load progress based on their advancement, access all other menu options, and exit the game.
  - `LevelCompleteManager.cs`: Handles level progression and checkpoint saving.
  - `LaserMovement.cs`: Manages the movement of player and enemy lasers.
  - `BossBehavior.cs`: Defines the logic for boss fights.
  - `PlayerController.cs`: Handles player input and movement.
  - `AsteroidSpawner.cs`: Spawns asteroids during gameplay.
  - `CutsceneManager.cs`: Controls the transition between gameplay and cutscenes.
  - `MusicManager.cs`: Manages the in-game music.
  - `OptionsMenu.cs`: Handles game settings, such as music and sound effects.
  - `Healthbar.cs`: Updates the player's and enemies' health bars.

---

## Testing

The game includes comprehensive unit and integration tests to ensure smooth gameplay and functionality. All tests are located in the `Assets/Tests` folder.

### Test Overview

1. **`BossBehaviorTest.cs`:**
   - Tests boss health reduction and destruction mechanics.

2. **`LaserMovementTest.cs`:**
   - Validates the movement of lasers fired by the player or enemies.

3. **`Menu_CheckpointSystemTest.cs`:**
   - Ensures the checkpoint system works as intended and locked levels cannot be accessed prematurely.

4. **`PlayerLivesTest.cs`:**
   - Tests the player's life counter and game-over logic.

### Running Tests

To execute the tests:
1. Open Unity and navigate to **Window > General > Test Runner**.
2. Select the desired test category (Edit Mode or Play Mode).
3. Run all tests or individual tests to verify the game's functionality.

---

## Conclusion

As Yoda would say, "Complete this README is, but your adventure it begins." With its pixel-art charm and Star Wars-themed challenges, **Star Wars - Rough Path - The Game** invites players to journey through asteroid fields, battle the Empire, and destroy the Death Star. May the Force be with you, always. And remember—those asteroids won't dodge themselves!
