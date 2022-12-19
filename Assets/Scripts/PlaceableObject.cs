using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private bool Placed;

    private void Awake(){
        Placed = false;
    }

    public bool GetPlaced(){
        return Placed;
    }

    public virtual void Place(){
        Placed = true;
        Grid_Building_System.current.MainTilemap.SetTile(Grid_Building_System.current.grid.WorldToCell(transform.position), Grid_Building_System.current.whiteTile);
        gameObject.AddComponent<ObjectDrag>();
        
    }
}
