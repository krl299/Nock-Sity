using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    private InputSystem inputActions;
    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;

    public LayerMask groundMask;

    public RoadManager roadManager;

    private void Awake()
    {
        inputActions = new InputSystem();

        inputActions.Player.Click.performed += ctx => Click();
        inputActions.Player.Click.canceled += ctx => ClickRealeased();
        inputActions.Enable();
    }

    private void Update()
    {
        ClickHold();
    }
    /// <summary>
    /// Casts a ray from the camera through the current mouse position. If the ray hits an object in the groundMask layer,
    /// it returns the hit point rounded to the nearest integer values as a Vector3Int. If the ray does not hit anything,
    /// it returns null.
    /// </summary>
    /// <returns></returns>
    private Vector3Int? RaycastGround()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int posInt = Vector3Int.RoundToInt(hit.point);
            return posInt;
        }
        return null;
    }

    private void ClickRealeased()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
            OnMouseUp?.Invoke();
        Debug.Log("Click released");
    }

    private void Click()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
                OnMouseClick?.Invoke((Vector3Int)position);
        Debug.Log("Click");
        }
    }

    private void ClickHold()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
                OnMouseHold?.Invoke((Vector3Int)position);
        Debug.Log("Click hold");
        }
    }
}
