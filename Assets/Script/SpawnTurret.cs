using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnTurret : MonoBehaviour
{
    Camera cam;
    Ray ray;
    RaycastHit2D hit;
    
    public float checkRadius = 1f;
    private TurretData selectedData;
    Vector2 spawnPosition;
    [SerializeField] LayerMask turretLayer;
    [SerializeField] LayerMask SpawnTurretLayer;

    private void OnEnable() => SpawnTurretUI.OnTurretSelected += SetSelected;
    private void OnDisable() => SpawnTurretUI.OnTurretSelected -= SetSelected;

    private void SetSelected(TurretData data) => selectedData = data;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        CameraControl();
       
       
       
       
    }

    private void CameraControl()
    {
        if (selectedData == null) return;
        Mouse mouse = Mouse.current;

        if (!mouse.leftButton.wasPressedThisFrame)
            return;

        Vector3 worldPosition = cam.ScreenToWorldPoint(mouse.position.ReadValue());
        worldPosition.z = -cam.transform.position.z;
        spawnPosition = new Vector2(worldPosition.x, worldPosition.y);

        Collider2D currentTarget = Physics2D.OverlapCircle(spawnPosition, checkRadius ,SpawnTurretLayer);
        if (currentTarget == null) return;

        Collider2D existingTurret = Physics2D.OverlapCircle(currentTarget.transform.position, checkRadius, turretLayer);
        if (existingTurret != null) return;



        if (GameManager.Instance.gold >= selectedData.Cost)
        {
            GameManager.Instance.RemoveGold(selectedData.Cost);
            SpawnPrefab(selectedData.turret, currentTarget.transform.position);
        }
        else Debug.LogWarning("vous n'avez pas assez de piece");

    }

    private void SpawnPrefab(GameObject turretGO,Vector3 position)
    {
        
        Instantiate(turretGO, position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(spawnPosition, checkRadius);
    }
}
