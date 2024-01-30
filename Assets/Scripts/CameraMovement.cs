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
        [SerializeField] private float cameraMovementSpeed;
        [SerializeField] private Vector2 moveDir;

        [Header("Camera Zoom")]
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        [SerializeField] private float currentZoom;

        [Header("Camera Rotation")]
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float minRotation;
        [SerializeField] private float maxRotation;
        [SerializeField] private Vector2 currentRotation;
        [SerializeField] private float currentRotationX;
        [SerializeField] private bool isRotating;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// It initializes the game camera, input actions, and sets up the event handlers for player actions siu.
        /// </summary>
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
            inputActions.Player.Enable();
        }

        /// <summary>
        /// Start is called before the first frame update.
        /// It sets the initial zoom and rotation of the camera.
        /// </summary>
        private void Start()
        {
            currentZoom = 45;
            currentRotationX = -50;

        }

        /// <summary>
        /// LateUpdate is called once per frame, after all Update functions have been called.
        /// It moves, zooms, and rotates the camera.
        /// </summary>
        private void LateUpdate()
        {
            MoveCamera(moveDir);
            ZoomCamera();
            RotateCamera();
        }

        /// <summary>
        /// Rotates the camera based on mouse input.
        /// </summary>
        private void RotateCamera()
        {
            float mouseX;
            if (!isRotating)
            {
                mouseX = 0;
                return;
            }
            mouseX = currentRotation.x;
            float mouseY = currentRotation.y;
            currentRotationX += -mouseY * rotationSpeed;
            currentRotationX = Mathf.Clamp(currentRotationX, minRotation, maxRotation);
            transform.eulerAngles = new Vector3(currentRotationX,
                transform.eulerAngles.y + (mouseX * rotationSpeed), 0);
        }

        /// <summary>
        /// Adjusts the camera's zoom level.
        /// </summary>
        private void ZoomCamera()
        {
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            if (!gameCamera.orthographic)
                gameCamera.fieldOfView = currentZoom;
        }

        /// <summary>
        /// Moves the camera based on the input vector.
        /// </summary>
        /// <param name="inputVector">The input vector, where x represents horizontal movement and y represents vertical movement.</param>
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