using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {   
        //calculate the vector the gameobject have moved
        offset = gameObject.transform.position - Grid_Building_System.GetMouseWorldPosition();
        //remove the white tile under it
        Grid_Building_System.current.MainTilemap.SetTile(Grid_Building_System.current.grid.WorldToCell(transform.position), null);
        Debug.Log("Mouse Down");
    }

    private void OnMouseDrag()
    {
        //add the moved vector to it's original vector to get the new vector
        Vector3 pos = Grid_Building_System.GetMouseWorldPosition() + offset;
        bool check = Grid_Building_System.current.CheckTileFromCoordinate(pos);
        //check if the new position is occupied or not, if occupied than don't move the game object
        if(check == false){
            transform.position = Grid_Building_System.current.SnapCoordinateTOGrid(pos);
        }
    }

    private void OnMouseUp()
    {
        //placed the game object once the user release the mouse
        Debug.Log("Mouse Up");
        Vector3 pos = Grid_Building_System.GetMouseWorldPosition() + offset;
        transform.position = Grid_Building_System.current.SnapCoordinateTOGrid(pos);
        //set the tile under it to white to indicate the tile is occupied
        Grid_Building_System.current.MainTilemap.SetTile(Grid_Building_System.current.grid.WorldToCell(transform.position), Grid_Building_System.current.whiteTile);
    }
}
