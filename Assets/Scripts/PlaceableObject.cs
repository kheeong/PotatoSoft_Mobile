using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private bool Placed;
    //some random code I think not nessarry, but just in case smt breaks :p
    private void Awake(){
        Placed = false;
    }

    public bool GetPlaced(){
        return Placed;
    }

    public virtual void Place(){
        Placed = true;
        //set the tile map under the game object to be white to indicate the tile have been occupied
        Grid_Building_System.current.MainTilemap.SetTile(Grid_Building_System.current.grid.WorldToCell(transform.position), Grid_Building_System.current.whiteTile);
        //add the ObjectDrag script to the game object once place
        gameObject.AddComponent<ObjectDrag>();
        
    }
}
