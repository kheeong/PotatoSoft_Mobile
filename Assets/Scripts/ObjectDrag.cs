using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Grid_Building_System.GetMouseWorldPosition();
        Grid_Building_System.current.MainTilemap.SetTile(Grid_Building_System.current.grid.WorldToCell(transform.position), null);
        Debug.Log("Mouse Down");
    }

    private void OnMouseDrag()
    {
        Vector3 pos = Grid_Building_System.GetMouseWorldPosition() + offset;
        bool check = Grid_Building_System.current.CheckTileFromCoordinate(pos);
        if(check == false){
            transform.position = Grid_Building_System.current.SnapCoordinateTOGrid(pos);
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up");
        Vector3 pos = Grid_Building_System.GetMouseWorldPosition() + offset;
        transform.position = Grid_Building_System.current.SnapCoordinateTOGrid(pos);
        Grid_Building_System.current.MainTilemap.SetTile(Grid_Building_System.current.grid.WorldToCell(transform.position), Grid_Building_System.current.whiteTile);
    }
}
