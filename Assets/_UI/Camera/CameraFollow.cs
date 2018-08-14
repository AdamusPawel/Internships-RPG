using RPG.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.CameraUI
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, -1, -1);

        [SerializeField] float minZoom = 4f;
        [SerializeField] float maxZoom = 13f;
        [SerializeField] float zoomSpeed = 800f;
        [SerializeField] float yawSpeed = 100f;

        [SerializeField] public float pitch = .7f;
        [SerializeField] float currentZoom = 13f;
        [SerializeField] float currentYaw = 135f;

        void Start()
        {
            SetTargetForCameraToFollow();
        }

        private void SetTargetForCameraToFollow()
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                Debug.LogError("There is no player in the scene for main camera to follow");
            }
        }

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
        }
    }
}