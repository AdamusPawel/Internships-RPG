using UnityEngine;

namespace RPG.CameraUI
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, -1, -1);

        public float pitch = .7f;

        public float minZoom = 4f;
        public float maxZoom = 13f;
        public float zoomSpeed = 800f;
        public float yawSpeed = 100f;

        public float currentZoom = 13f;
        public float currentYaw = 135f;

        void Update()
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        }

        void LateUpdate()
        {
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);

            transform.RotateAround(target.position, Vector3.up, currentYaw);

            Debug.Log("Current zoom = " + currentZoom + ", current yaw = " + currentYaw % 360);
        }
    }
}