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
    }
    #endregion

    #region Field
    public TextMeshProUGUI timeText;
    public GameObject gameOverUI;

    //치트키 활성화
    [SerializeField] private bool ActivateCheat = false;

    private float surviveTime = 0f;
    private static bool isGameOver = false;
    #endregion

    #region Property
    public static bool IsGameOver
    {
        get { return isGameOver; }
    }
    #endregion

    private void Start()
    {
        isGameOver = false;
    }

    void Update()
    {

        if (isGameOver) return;

        surviveTime += Time.deltaTime;
        timeText.text = "Time: " + surviveTime.ToString("F2");

        //치트
        if (Input.GetKeyDown(KeyCode.K) && ActivateCheat)
        {
            PlayerDied();
        }
    }

    public void PlayerDied()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverUI.SetActive(true);

        // 마우스 커서 다시 활성화
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f; // 게임 정지
    }

}
