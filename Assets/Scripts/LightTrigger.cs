using LightDodger;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    #region Field
    public Material chaseModeMaterial;

    private Material startMaterial;
    #endregion
    private void Start()
    {
        //초기화
        startMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        // 혹시 이거 업데이트에 넣으면 안좋은가?
        gameObject.GetComponent<MeshRenderer>().material = (PatrolLight.IsChaseMode) ? chaseModeMaterial : startMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("빛에 닿음!");
            GameManager.Instance.PlayerDied();  // 게임매니저에게 알림
        }
    }
}
