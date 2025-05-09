using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GAEditor : MonoBehaviour
{
    public Button resetButton;
    public Button generateAnimationButton;

    void OnResetButtonPressed()
    {
        Debug.Log("pressed");
        MoveLogger.ResetJson();
        SceneManager.LoadScene("StartScreenUI");
    }

    void OnGenerateAnimationButtonPressed()
    {
        Debug.Log("pressed");
        SceneManager.LoadScene("Chess3dPart");
    }

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnResetButtonPressed);
        }
        if (generateAnimationButton != null)
        {
            generateAnimationButton.onClick.AddListener(OnGenerateAnimationButtonPressed);
        }
    }
}
