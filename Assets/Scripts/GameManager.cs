using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    void Awake()
    {
        // 중복 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
    }
    #endregion

    #region Field
    public TextMeshProUGUI timeText;
    public GameObject gameOverUI;

    private float surviveTime = 0f;
    private bool isGameOver = false;
    #endregion


    void Update()
    {
        if (isGameOver) return;

        surviveTime += Time.deltaTime;
        timeText.text = "Time: " + surviveTime.ToString("F2");
    }

    public void PlayerDied()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // 게임 정지
    }
}
