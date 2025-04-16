using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightDodger
{
    public class GameOverUI : MonoBehaviour
    {
        public void Retry()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //빌드 인덱스로 로드
        }

        public void Menu()
        {
            Debug.Log("메뉴 실행");
        }
    }

}
