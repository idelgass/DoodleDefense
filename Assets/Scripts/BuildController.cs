using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;


public class BuildController : SingletonMonoBehavior<BuildController>
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private LayerMask tilemapLayerMask;
    [SerializeField] private LayerMask UILayerMask;
    [SerializeField] private TileBase buildTile; // Tile it's okay to build on

    // public GameObject TurretPrefab { get; set; }

    private bool isValid;
    private bool isTurretLoaded;
    private GameObject iconHolder;
    private SpriteRenderer iconRenderer;
    private GameObject turretPrefab;

    public void ActivateCursor(TurretData turretData)
    {
        iconHolder.GetComponent<SpriteRenderer>().sprite = turretData.turretIcon;
        turretPrefab = turretData.turretPrefab;
        iconHolder.SetActive(true);
        isTurretLoaded = true;
    }

    private bool CheckValidity(Vector3 position)
    {

        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(position);

        // Don't want to place a turret under a button (including other turrets)
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Mouse is over a UI element");
            iconRenderer.color = Color.red;
            return false;
        }

        RaycastHit2D tileHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, tilemapLayerMask);

        if (tileHit.collider != null)
        {
            if (tileHit.collider.gameObject.GetComponent<Tilemap>() == tilemap)
            {
                Vector3Int tilePos = tilemap.WorldToCell(tileHit.point);

                TileBase tile = tilemap.GetTile(tilePos);

                if (tile != null  && tile.name == buildTile.name)
                {
                    // Debug.Log("Valid tile: " + tile.name);
                    iconRenderer.color = Color.green;
                    return true;
                }
                else
                {
                    // Debug.Log("Invalid tile: " + tile.name);
                }
            }
        }
        iconRenderer.color = Color.red;
        return false;
    }

    private void CursorFollow(Vector2 cursorPos)
    {
        iconHolder.transform.position = cursorPos;
    }

    private void Reset()
    {
        iconHolder.SetActive(false);
        UIController.Instance.CloseShop();
        isTurretLoaded = false;
    }

    private void Start()
    {
        isValid = false;
        iconHolder = new GameObject("iconHolder");
        iconHolder.AddComponent<SpriteRenderer>();
        iconRenderer = iconHolder.GetComponent<SpriteRenderer>(); 
        iconRenderer.sortingLayerName = "Turrets";
        iconHolder.transform.localScale = new Vector3(4f, 4f, 0f);

        iconHolder.SetActive(false);
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        if(isTurretLoaded)
        {
            isValid = CheckValidity(mousePos);
            CursorFollow(cursorPos);
            if(isValid && Input.GetMouseButtonDown(0))
            {
                Instantiate(turretPrefab, cursorPos, Quaternion.identity);
                Reset();
                Debug.Log(isValid);
            }
        }
                

    }

}
