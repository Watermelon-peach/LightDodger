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
        //스크롤
        public float scrollMin = 10f;
        public float scrollMax = 20f;

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
            UpDown();
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

        void UpDown()
        {
            //시야 UpDown
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            Vector3 upMove = cameraTransform.position;
            upMove.y -= (scroll * 1000) * Time.deltaTime * 3f;

            upMove.y = Mathf.Clamp(upMove.y, scrollMin, scrollMax); //(pos.y : 10f 이상, 40f 이하)
            cameraTransform.position = upMove;
        }

        void LookAround()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }
    }

}
