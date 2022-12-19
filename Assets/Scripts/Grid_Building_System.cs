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
        //press A to generate the prefab
        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefab1);
        }
        //check if the object to place have been place for the first time or not
        if(objectToPlace != null){
            if(objectToPlace.GetPlaced() == false)
                {
                    //if not be placed yet, transfor the object according to the mouse location
                    Vector3 offset = objectToPlace.transform.position - GetMouseWorldPosition();
                    Vector3 position = SnapCoordinateTOGrid(GetMouseWorldPosition());
                    //check if the mouse location tile is a white tile, if yes it indicate the tile have been used hence will not mova the gameobject on to that tile
                    bool check = Grid_Building_System.current.CheckTileFromCoordinate(position);
                    if(check == false){
                        objectToPlace.transform.position = position;
                    }
                    //place the game object after the left mouse button is released
                    if (Input.GetMouseButtonUp(0))
                    {
                        //call the class to place the object
                        objectToPlace.Place();
                    }
                    if (Input.GetMouseButtonUp(1)){
                        //remove object if right click
                        Debug.Log("Destroying");
                        Destroy(objectToPlace.gameObject);
                    }
                }
        }
    }
    //get the name of the game object if the mouse is pointing at one
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
    //get the world position of the mouse is pointing aka XYZ
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
    // locate XYZ position of the center of the tile given at a XYZ position to place the game object
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
    //check if the tile is already occupied base on XYZ coordiante
    public bool CheckTileFromCoordinate(Vector3 coordinate)
    {
        Vector3Int cell = gridLayout.WorldToCell(coordinate);
        bool ans = MainTilemap.HasTile(cell);
        return ans;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        //get the position of the place where the mouse is pointing
        Vector3 position = SnapCoordinateTOGrid(GetMouseWorldPosition());
        //spawn the game object from prefab
        GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
        //add the PlaceableOject script to the object
        objectToPlace = newObject.gameObject.GetComponent<PlaceableObject>();
    }
    
}


