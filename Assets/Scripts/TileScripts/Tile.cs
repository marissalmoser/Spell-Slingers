using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    /// utility
    Vector2Int coordinates;
    [SerializeField] Transform actorAnchor;

    ///tile state
    public enum TileState { idle, moveable, attackable };
    private TileState currentState = TileState.idle;
    //TODO: add tile effect field

    ///actions
    public static Action<Tile> TileSelected;

    /// <summary>
    /// Sets the corrdinates of the tile. Should only be used by the tile manager.
    /// </summary>
    public void SetCoordinates(int x, int y)
    {
        coordinates = new Vector2Int(x, y);
    }

    private void OnMouseUpAsButton()
    {
        TileSelected?.Invoke(this);
    }
    
    /// <summary>
    /// Call this function to add an effect to this tile. This function will evaluate 
    /// if it should call the ability attack or combo and set the visuals accordingly.
    /// </summary>
    public void AddEffect()
    {
        //TODO: Add ability parameter
        //TODO: Add ability evaluation and functionality
    }

    /// <summary>
    /// This function will update the tile's current state and visuals
    /// </summary>
    /// <param name="newState">State to change tile to</param>
    public void SetState(TileState newState)
    {
        //TODO: set up visuals for each state
    }
}
