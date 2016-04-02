using UnityEngine;
using System.Collections;

/* This script controls the player. Allows the player to be controlled by user with
 * the four arrow keys. Pressing one of the four arrow keys will cause the player
 * to face that direction and either move down onto the next tile in that direction,
 * or to jump up onto the next tile in that direction. If player falls off of the
 * game world, they are respawned at initial position. */
public class PlayerController : MonoBehaviour 
{
    // Represent the four movement directions
    Quaternion NORTHWEST;
    Quaternion NORTHEAST;
    Quaternion SOUTHEAST;
    Quaternion SOUTHWEST;

    Vector3 movementDirection;  // Stores the current movement direction
    float forwardMovement;      // Distance one key press moves character in game
    public float jumpHeight;

    Vector3 respawnPoint;
    Quaternion respawnDirection;
	
	void Start() 
    {
        // Movement directions as rotations about the y-axis
        NORTHWEST = Quaternion.Euler(0.0f, 315.0f, 0.0f);
        NORTHEAST = Quaternion.Euler(0.0f, 45.0f, 0.0f);
        SOUTHEAST = Quaternion.Euler(0.0f, 135.0f, 0.0f);
        SOUTHWEST = Quaternion.Euler(0.0f, 225.0f, 0.0f);

        // Store direction character is currenty facing
        movementDirection = transform.TransformDirection(Vector3.forward);

        // Move player distance of one tile in game
        forwardMovement = 1.0f;

        // Store initial position of player as respawn point
        respawnPoint = transform.position;
        respawnDirection = SOUTHEAST;
	}
	
	void Update() 
    {
        // Check if player has fallen off of grid
        if (transform.position.y < 0)
            RespawnPlayer();

        // Left arrow moves northwest
        if (Input.GetKeyDown(KeyCode.LeftArrow) )
            MovePlayer(NORTHWEST, true);

        // Up arrow moves northeast
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            MovePlayer(NORTHEAST, true);
        
        // Right arrow moves southeast
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            MovePlayer(SOUTHEAST, false);

        // Down arrow moves southwest
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            MovePlayer(SOUTHWEST, false);   
	}

    /* Causes player to face rotation direction and move forward one tile
     * in that direction. If jumping, causes player to jump up and forward */
    void MovePlayer(Quaternion rotation, bool isJumping)
    {
        transform.rotation = rotation;
        movementDirection = transform.TransformDirection(Vector3.forward);

        if (isJumping)
            transform.Translate(Vector3.up * jumpHeight * Time.deltaTime, Space.World);
        
        transform.position += (movementDirection * forwardMovement);
    }

    /* Causes player GameObject to repawn at the top of the grid when they fall off
     of the tiles, as well as lose a life */
    void RespawnPlayer()
    {
        // call to method controlling life/score counts
        transform.position = respawnPoint;
        transform.rotation = respawnDirection;
    }
}
