using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TurretSelector : MonoBehaviour
{
    [SerializeField] LayerMask turretLayer;
    Camera cam;
    bool isPlacing;

    public static event Action<TurretLeveler> OnTurretClicked;

    private void OnEnable() => SpawnTurretUI.OnTurretSelected += HandleCardSelected;
    private void OnDisable() => SpawnTurretUI.OnTurretSelected -= HandleCardSelected;

    private void HandleCardSelected(TurretData data)
    {
     isPlacing = data != null;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(isPlacing ) return;
        Mouse mouse = Mouse.current;
        Vector3 worldPosition = cam.ScreenToWorldPoint(mouse.position.ReadValue());
        if (OnTurretClicked == null) return;
        worldPosition.z = -cam.transform.position.z;

        if (!mouse.rightButton.wasPressedThisFrame) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        

       

        Collider2D coll = Physics2D.OverlapPoint(worldPosition, turretLayer);
        if (coll != null )
        {
           if(coll.TryGetComponent(out TurretLeveler leveler)) OnTurretClicked(leveler);
        }
        else
        {
            OnTurretClicked?.Invoke(null);   
        }
    }
}
