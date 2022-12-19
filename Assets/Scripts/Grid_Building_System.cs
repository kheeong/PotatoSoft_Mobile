using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Grid_Building_System : MonoBehaviour
{
    // Start is called before the first frame update
    public static Grid_Building_System current;

    public GridLayout gridLayout;
    public Grid grid;
    [SerializeField] public Tilemap MainTilemap;
    [SerializeField] public TileBase whiteTile;

    public GameObject prefab1;

    private PlaceableObject objectToPlace;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefab1);
        }
        if(objectToPlace != null){
            if(objectToPlace.GetPlaced() == false)
                {
                    Vector3 offset = objectToPlace.transform.position - GetMouseWorldPosition();
                    Vector3 position = SnapCoordinateTOGrid(GetMouseWorldPosition());
                    bool check = Grid_Building_System.current.CheckTileFromCoordinate(position);
                    if(check == false){
                        objectToPlace.transform.position = position;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        objectToPlace.Place();
                    }
                    if (Input.GetMouseButtonUp(1)){
                        Debug.Log("Destroying");
                        Destroy(objectToPlace.gameObject);
                    }
                }
        }
    }
    public static string GetMouseObjectName(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject.name;
        }
        else
        {
            return "Nothing";
        }
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateTOGrid(Vector3 coordinate)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(coordinate);
        Vector3 position = grid.CellToWorld(cellPosition);
        position[1] = 0.5f;
        position[0] = position[0] + 0.5f;
        position[2] = position[2] + 0.5f;
        Debug.Log(MainTilemap.HasTile(cellPosition));
        return position;
    }

    public bool CheckTileFromCoordinate(Vector3 coordinate)
    {
        Vector3Int cell = gridLayout.WorldToCell(coordinate);
        bool ans = MainTilemap.HasTile(cell);
        return ans;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateTOGrid(GetMouseWorldPosition());
        GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = newObject.gameObject.GetComponent<PlaceableObject>();
    }
    
}


