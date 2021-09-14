using UnityEngine;

namespace FormulaManager.Camera
{
    public class Selectable : MonoBehaviour
    {
        private GameObject cameraObj;
        private CameraController controller;

        private void Start()
        {
            cameraObj = UnityEngine.Camera.main.gameObject.transform.parent.gameObject;
            controller = cameraObj.transform.GetComponent<CameraController>();
        }

        private void OnMouseDown()
        {
            controller.SetFollowTarget(transform);
        }
    }
}