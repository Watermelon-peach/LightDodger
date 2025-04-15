using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LightDodger
{
    public class PatrolLight : MonoBehaviour
    {
        #region Field
        //패트롤 포인트 지정 경로를 부모 오브젝트로 불러오기
        public Transform waypointParent;
        private List<Transform> waypoints = new List<Transform>();

        // 체이스모드 시 플레이어 추적
        public Transform targetPlayer;
        public float sensorRange = 10f;
        public float acceleration = 5f;

        public float startSpeed;
        public float moveSpeed = 2f;
        public float waitTime = 2f;
        public float lookAroundAngle = 45f;
        public float lookAroundSpeed = 50f;

        private int currentIndex = 0;
        private bool isLookingAround = false;
        private float waitTimer = 0f;
        private float currentAngle = 0f;
        private int lookDir = 1;
        private int direction = 1; // 1이면 정방향, -1이면 역방향
        private static bool isChaseMode;   //ChaseMode on/off
        #endregion

        #region Property
        //읽기전용속성
        public static bool IsChaseMode
        {
            get { return isChaseMode; }
        }
        #endregion
        void Start()
        {
            // 자식(경로)들 불러오기
            for (int i = 0; i < waypointParent.childCount; i++)
            {
                waypoints.Add(waypointParent.GetChild(i));
            }
            //ChaseMode 초기화
            isChaseMode = false;
            startSpeed = moveSpeed;
        }

        void Update()
        {
            //범위내 플레이어 감지
            UpdateTarget();

            // 패트롤모드
            //경로가 최소 2개 이상일 때, isChaseMode가 false일 때만 작동
            if (waypoints.Count < 2 || isChaseMode) return;
            if (isLookingAround)
                LookAround();
            else
                MoveToWaypoints();
        }

        void UpdateTarget()
        {
            float distance = Vector3.Distance(transform.position, targetPlayer.position);
            if (distance <= sensorRange)
            {
                //체이스모드 on
                isChaseMode = true;
                ChaseMode();
            }
            else
            {
                //체이스모드 off
                isChaseMode = false;
                moveSpeed = startSpeed;
            }
        }
        //기즈모
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, sensorRange);
        }
        void ChaseMode()
        {
            moveSpeed = Mathf.Lerp(moveSpeed, startSpeed*4, Time.deltaTime * acceleration);
            Vector3 dir = (targetPlayer.position - transform.position).normalized;
            dir.y = 0;
            transform.Translate(dir * Time.deltaTime * moveSpeed,Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        }

        void MoveToWaypoints()
        {
            Vector3 targetPos = waypoints[currentIndex].position; //현재 목표 지정

            // 목표 지점까지 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            Vector3 moveDir = (targetPos - transform.position).normalized;
            if (moveDir != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // 도착 체크
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                // 양 끝 인덱스(0, 마지막)일 때만 두리번 시작
                if (currentIndex == 0 || currentIndex == waypoints.Count - 1)
                {
                    isLookingAround = true;
                    waitTimer = 0f;
                    currentAngle = 0f;
                    lookDir = 1;
                }

                // 방향 바꾸기 (끝에 도달했을 때)
                if (currentIndex == waypoints.Count - 1)
                    direction = -1;
                else if (currentIndex == 0)
                    direction = 1;

                // 다음 목표 인덱스로 이동
                currentIndex += direction;
            }
        }

        void LookAround()
        {
            waitTimer += Time.deltaTime;

            // 좌우 회전 (lookDir 방향으로)
            float rotateAmount = lookAroundSpeed * Time.deltaTime * lookDir;
            transform.Rotate(Vector3.up * rotateAmount, Space.Self);
            currentAngle += Mathf.Abs(rotateAmount);

            // 설정된 각도만큼 돌았으면 반대 방향으로
            if (currentAngle >= lookAroundAngle)
            {
                lookDir *= -1;
                currentAngle = 0f;
            }

            // 일정 시간 지나면 두리번 끝내고 다시 이동
            if (waitTimer >= waitTime)
            {
                isLookingAround = false;
            }
        }
    }

}