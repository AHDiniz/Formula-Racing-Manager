using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FormulaManager.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform followTarget;
        [Header("Movement")]
        [SerializeField] private float normalSpeed, fastSpeed;
        [SerializeField] private KeyCode fastSpeedKey = KeyCode.LeftShift;
        [SerializeField] private float movementLerpTimeScale;
        [Header("Rotation")]
        [SerializeField] private float rotationAmount;
        [Header("Zooming")]
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Vector3 zoomAmount;
        [SerializeField] private float minDistance, maxDistance;

        private bool canMove = true;
        private float movementSpeed;
        private Vector3 newPosition, newZoom;
        private Vector3 dragStart, dragCurrent;
        private Vector3 rotateStart, rotateCurrent;
        private Quaternion newRotation;

        private void Start()
        {
            newPosition = transform.position;
            newRotation = transform.rotation;
            newZoom = cameraTransform.localPosition;
        }

        private void Update()
        {
            if (followTarget != null)
                transform.position = followTarget.position;
            else if (canMove)
                HandleMovementInput();

            if (canMove)
            {
                HandleRotationInput();
                HandleZoomInput();
            }
            
            if (Input.GetKey(KeyCode.Escape) && canMove) followTarget = null;
        }

        public void SwitchCanMoveOnOff()
        {
            canMove = !canMove;
        }

        private void HandleMovementInput()
        {
            movementSpeed = Input.GetKey(fastSpeedKey) ? fastSpeed : normalSpeed;

            if (Input.GetMouseButtonDown(0))
            {
                Plane p = new Plane(Vector3.up, Vector3.zero);
                Ray r = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;
                if (p.Raycast(r, out entry)) dragStart = r.GetPoint(entry);
            }

            if (Input.GetMouseButton(0))
            {
                Plane p = new Plane(Vector3.up, Vector3.zero);
                Ray r = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;
                if (p.Raycast(r, out entry))
                {
                    dragCurrent = r.GetPoint(entry);
                    newPosition = transform.position + (dragStart - dragCurrent);
                }
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                newPosition += transform.forward * movementSpeed;
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                newPosition += transform.forward * -movementSpeed;
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                newPosition += transform.right * movementSpeed;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                newPosition += transform.right * -movementSpeed;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementLerpTimeScale);
        }

        private void HandleRotationInput()
        {
            if (Input.GetMouseButtonDown(1)) rotateStart = Input.mousePosition;

            if (Input.GetMouseButton(1))
            {
                rotateCurrent = Input.mousePosition;
                Vector3 diff = rotateStart - rotateCurrent;
                rotateStart = rotateCurrent;
                newRotation *= Quaternion.Euler(Vector3.up * (-diff.x / 5));
            }

            if (Input.GetKey(KeyCode.Q)) newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);

            if (Input.GetKey(KeyCode.E)) newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementLerpTimeScale);
        }

        private void HandleZoomInput()
        {
            if (Input.mouseScrollDelta.y != 0) newZoom += -Input.mouseScrollDelta.y * zoomAmount;

            if (Input.GetKey(KeyCode.R)) newZoom += zoomAmount;

            if (Input.GetKey(KeyCode.F)) newZoom -= zoomAmount;

            newZoom.y = Mathf.Clamp(newZoom.y, minDistance, maxDistance);
            newZoom.z = -Mathf.Clamp(Mathf.Abs(newZoom.z), minDistance, maxDistance);

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementLerpTimeScale);
        }

        public void SetFollowTarget(Transform target)
        {
            followTarget = target;
        }
    }
}