using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{

    public class CameraMovement : MonoBehaviour
    {
        private InputSystem inputActions;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private float cameraMovementSpeed = 10f;
        [SerializeField] private Vector2 moveDir;

        [Header("Camera Zoom")]
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float minZoom = 5f;
        [SerializeField] private float maxZoom = 20f;
        [SerializeField] private float currentZoom;

        [Header("Camera Rotation")]
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float minRotation = 0f;
        [SerializeField] private float maxRotation = 90f;
        [SerializeField] private Vector2 currentRotation;
        [SerializeField] private float currentRotationX;
        [SerializeField] private bool isRotating;

        private void Awake()
        {
            gameCamera = GetComponentInChildren<Camera>();

            inputActions = new InputSystem();

            inputActions.Player.Move.performed += ctx => moveDir = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => moveDir = Vector2.zero;
            inputActions.Player.Zoom.performed += ctx => currentZoom += ctx.ReadValue<Vector2>().y * zoomSpeed;
            inputActions.Player.Rotate.performed += ctx => isRotating = true;
            inputActions.Player.Rotate.canceled += ctx => isRotating = false;
            inputActions.Player.Look.performed += ctx => currentRotation = ctx.ReadValue<Vector2>();
            inputActions.Player.Look.canceled += ctx => currentRotation = Vector2.zero;
            inputActions.Enable();
        }

        private void Start()
        {
            currentZoom = gameCamera.transform.localPosition.y;
            currentRotationX = -50;
        }

        private void LateUpdate()
        {
            MoveCamera(moveDir);
            ZoomCamera();
            RotateCamera();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RotateCamera()
        {
            if (!isRotating)
                return;
            currentRotationX += -currentRotation.y * rotationSpeed; 
            currentRotationX = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
            transform.eulerAngles = new Vector3(currentRotationX,
                transform.eulerAngles.y + (currentRotation.x * rotationSpeed), 0);
        }

        //TODO: Fix zooming in and out when rotating the camera
        /// <summary>
        /// 
        /// </summary>
        private void ZoomCamera()
        {
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            gameCamera.transform.localPosition = Vector3.up * currentZoom;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputVector"></param>
        public void MoveCamera(Vector2 inputVector)
        {
            Vector3 forward = gameCamera.transform.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = gameCamera.transform.right;
            Vector3 dir = forward * inputVector.y + right * inputVector.x;

            transform.position += dir * Time.deltaTime * cameraMovementSpeed;
        }
    }
}