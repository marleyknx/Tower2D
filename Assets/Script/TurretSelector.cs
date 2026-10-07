using System;
using System.Collections.Generic;
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
        Mouse mouse = Mouse.current;
        if (!mouse.rightButton.wasPressedThisFrame) return;
        Debug.Log("1. clic reçu");

        if (isPlacing) { Debug.Log("STOP : isPlacing"); return; }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = mouse.position.ReadValue();

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results);

            foreach (RaycastResult r in results)
                Debug.Log("UI sous la souris : " + r.gameObject.name);
            return;
        }

        Vector3 worldPosition = cam.ScreenToWorldPoint(mouse.position.ReadValue());
        Collider2D coll = Physics2D.OverlapPoint(worldPosition, turretLayer);
        Debug.Log("2. collider touché : " + (coll != null ? coll.name : "rien"));

        if (coll != null && coll.TryGetComponent(out TurretLeveler leveler))
        {
            Debug.Log("3. event envoyé");
            OnTurretClicked?.Invoke(leveler);
        }
        else OnTurretClicked?.Invoke(null);
    }
}
