using UnityEngine;
using System.Collections;

/* This script keeps track of and monitors when a tile has been visited by a player 
 * GameoObject as well as how many times that tile has been visited. The tile will 
 * change its colour based on visits. */
public class Tile : MonoBehaviour 
{
    public Color defaultColor;  // Unvisited tiles have this colour
    public Color visitedColor;  // Tile changes to this colour once visited
    Material tileMaterial;      // Material attached to gameObject

    bool wasVisited;            // Keep track of whether player has visited this tile
    int timesVisited;           // The number of times this tile has been visited

	void Start () 
    {
        // Get tile's MeshRenderer so we can get and manipulate its material
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
            tileMaterial = renderer.material;

        wasVisited = false;
        timesVisited = 0;
	}
	
	void Update () 
    {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            wasVisited = true;
            ++timesVisited;
            tileMaterial.color = visitedColor;
        }
    }
}
