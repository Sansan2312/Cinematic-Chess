using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
    public Button PlayAgain;
    public Button Restart;

    void OnRestartButtonPressed()
    {
        MoveLogger.ResetJson();
        SceneManager.LoadScene("StartScreenUI");
    }

    void OnPlayAgainButtonPressed()
    {
        SceneManager.LoadScene("Chess3dPart");
    }

    private void Start()
    {
        if (Restart != null)
        {
            Restart.onClick.AddListener(OnRestartButtonPressed);
        }
        if (PlayAgain != null)
        {
            PlayAgain.onClick.AddListener(OnPlayAgainButtonPressed);
        }
    }

}
