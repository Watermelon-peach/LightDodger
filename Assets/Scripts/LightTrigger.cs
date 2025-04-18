using LightDodger;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    #region Field
    public Material chaseModeMaterial;

    private MeshRenderer meshRenderer;
    private Material startMaterial;
    #endregion
    private void Start()
    {
        //참조
        meshRenderer = this.GetComponent<MeshRenderer>();
        //초기화
        startMaterial = meshRenderer.material;
    }

    private void Update()
    {
        meshRenderer.material = (PatrolLight.IsChaseMode)? chaseModeMaterial : startMaterial;
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
