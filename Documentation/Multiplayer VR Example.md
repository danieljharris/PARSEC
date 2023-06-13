# Multiplayer VR Example

PARSEC can be used as an example for how to implement VRTK with Photon Fusion to create multiplayer VR experances.

The pre-configured environments and components used to create a VR application are discussed in this document.

### Example Scene
Location: Assets/Prototype1/Scenes/Networking Example

This scene provides a barebones example of a multiplayer VR application using VRTK v4 Tilia and Photon Fusion

The scene loads up to 4 players into a scene alongside an cube that can be picked up, moved, and rotated by the players.

### Components
There are 3 components responable the network session of the VR players:
- Network Runner - Provided by Photon Fusion
- Network Runner Handler - Ensures there is only a single instance of the network runner in the scene and calls the Network Runner to start a join the session in "Shared Mode"
- Network Spawn Rig - Spawns the player prefab for each player who connects to the session

<img height="400" src="https://github.com/danieljharris/PARSEC/assets/1362512/50cd5122-4c6d-46b1-b115-f9cf268dcf46">

### Player Prefab
The player prefabs are configured differently depending on if they are spawned as a local or remote player. The differences between these is set in the "Network Player" component.

<img height="900" src="https://github.com/danieljharris/PARSEC/assets/1362512/5ac436e1-b4d3-4e73-ba7d-f51986cfcc03">
