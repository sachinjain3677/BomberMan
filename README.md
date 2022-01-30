# BomberMan
Motivated from the classic nintendo game "Bomberman", this game is built upon Unity3D and Vuforia to provide an immersive 3D Augmented Reality experience on your android device.

To play the game, install this [apk](https://drive.google.com/file/d/1jckp1iFQOsCMAKrhyYkkTFF_RA3aZ2Mn/view) on your android device, run the game and aim it at the image target provided in the Resources directory, enjoy!! Do let us know if you like the game, by giving us a star, thanks :)

Assets taken from the [Unity asset store](https://assetstore.unity.com/?gclid=Cj0KCQiAi9mPBhCJARIsAHchl1z52KMDOvktJJuYpZwUmGRrSgj2Kuupn0Ychu8Rh-yOXi5ARzhbVhAaAsC_EALw_wcB&gclsrc=aw.ds)

## First Look
### Main_Menu
![image](https://user-images.githubusercontent.com/24416093/151710321-9f92c2cd-94ce-43b2-a40b-a6c7ba620381.png)
### Gameplay
![image](https://user-images.githubusercontent.com/24416093/151710482-52f3e086-8f68-4796-aa0e-2ec5de56e5db.png)
![image](https://user-images.githubusercontent.com/24416093/151710494-c9ca9d19-dd7c-4f62-be77-07a16cb14608.png)
![image](https://user-images.githubusercontent.com/24416093/151710493-7f7d8c3b-a407-4bb0-b83b-914fafb3ea80.png)
### Pause_Menu![image](https://user-images.githubusercontent.com/24416093/151710495-342acef2-11c3-466b-996b-5d5031c0caf6.png)


## Steps involved in development:
1. **Basic character movements -** walking, rotating, bomb placement
2. **Enemy Movement -** Auto direction change, dynamic speeds depending on difficulty, random spawns
3. **Bomb -** pre-blast animation, dynamic blast region, detection and impact of the blast on the surroundings
4. **Obstacles -** wooden boxes containing health, bombs and even enemies sometimes
5. **Clock -** The whole game is a race against the time and generates score based on the number of obstacles cleared and enemies destroyed
6. **AR Integration -** Shifting the whole to AR and making some AR specific changes like what happens to the assets when image target is out of scope 
7. **Leaderboard -** After each game, the score is updated on a firebase server running in the cloud to keep track of the global leaderboard


## Future Scopes:
1. This game can be upgraded to work on any plane surface rather than just a specific image target
2. The game can be made progressive by adding levels and putting a win condition on each level
