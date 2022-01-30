# BomberMan
Motivated from the classic nintendo game "Bomberman", this game is built upon Unity3D and Vuforia to provide an immersive 3D Augmented Reality experience on your android device.

To play the game, install the apk in your android device, run the game and aim it at the image target provided, enjoy!

Assets taken from the [Unity asset store](https://assetstore.unity.com/?gclid=Cj0KCQiAi9mPBhCJARIsAHchl1z52KMDOvktJJuYpZwUmGRrSgj2Kuupn0Ychu8Rh-yOXi5ARzhbVhAaAsC_EALw_wcB&gclsrc=aw.ds)

## Steps involved in development:
1. **Basic character movements -** walking, rotating, bomb placement
2. **Enemy Movement -** Auto direction change, dynamic speeds depending on difficulty, random spawns
3. **Bomb -** pre-blast animation, dynamic blast region, detection and impact of the blast on the surroundings
4. **Obstacles -** wooden boxes containing health, bombs and even enemies sometimes
5. **Clock -** The whole game is a race against the time and generates score based on the number of obstacles cleared and enemies destroyed
6. **Leaderboard -** After each game, the score is updated on a firebase server running in the cloud to keep track of the global leaderboard


## Future Scopes:
1. This game can be upgraded to work on any plane surface rather than just a specific image target
2. The game can be made progressive by adding levels and putting a win condition on each level
