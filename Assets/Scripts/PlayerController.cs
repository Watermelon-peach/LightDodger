using UnityEngine;

namespace LightDodger
{
    public class PlayerController : MonoBehaviour
    {
        #region Field
        //필드
        public float startSpeed = 5f;
        public float moveSpeed;
        public float mouseSensitivity = 2f;
        public Transform cameraTransform; // 1인칭 카메라

        private Rigidbody rb;
        private float runSpeed;
        #endregion

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true; // 플레이어가 넘어지지 않도록 회전 고정
            moveSpeed = startSpeed;
            runSpeed = startSpeed * 2;

            Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 중앙 고정
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            LookAround();
        }

        void Move()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            //달리기
            moveSpeed = Input.GetKey(KeyCode.LeftShift)? 10f : startSpeed;
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }

        

        void LookAround()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }
    }

}
