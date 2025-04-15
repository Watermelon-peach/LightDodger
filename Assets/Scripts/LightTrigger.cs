using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("빛에 닿음!");
            GameManager.Instance.PlayerDied();  // 게임매니저에게 알림
        }
    }
}
