using UnityEngine;

namespace LightDodger
{
    public class ChasingLight : MonoBehaviour
    {
        #region Field
        public Transform targetTransform;
        public float moveSpeed = 2f;
        #endregion

        private void Update()
        {
            Chase(targetTransform);
        }

        void Chase(Transform target)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.Self);
            
        }
    }

}
