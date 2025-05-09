using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIElements : MonoBehaviour
{
    public GameObject resignButtonWhite;
    public GameObject resignButtonBlack;
    public GameObject resignButtonBackground;

    public GameObject winMessagePanel;
    public GameObject generateAnimationButton;
    public GameObject generateAnimationButtonBackground;
    public GameObject resultScreen;
    public GameObject whiteWinsText;
    public GameObject blackWinsText;

    private GameObject winsText;

    private GameObject buttonBackgroundInstance;

    public static float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }

    public void AnimateWinMessage(Vector3 position)
    {
        StartCoroutine(AnimateWinMessageCororutine(position));
    }
    private IEnumerator AnimateWinMessageCororutine(Vector3 position)
    {
        Vector3 resultScreenPos = transform.position + new Vector3(0.5f, 1f, -2f);
        GameObject resultScreenInstance = Instantiate(resultScreen, resultScreenPos, Quaternion.identity);
        resultScreenInstance.transform.localScale = Vector3.zero;

        Vector3 winsTextPos = resultScreenPos + new Vector3(0f, 3f, -3f);
        GameObject winsTextInstance = Instantiate(winsText, winsTextPos, Quaternion.identity);
        winsTextInstance.transform.localScale = Vector3.zero;

        Vector3 generateAnimationButtonPos = resultScreenPos + new Vector3(0f, -3.5f, -3f);
        GameObject generateAnimationButtonInstance = Instantiate(generateAnimationButton, generateAnimationButtonPos, Quaternion.identity);

        float duration = 1f;
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one * 10f;


        while (elapsed < duration)
        {
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);
            float scaleResultScreen = Map(t, 0f, 1f, 0f, 3f);
            winMessagePanel.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            resultScreenInstance.transform.localScale = new Vector3(scaleResultScreen, scaleResultScreen);
            winsTextInstance.transform.localScale = new Vector3(scaleResultScreen, scaleResultScreen);
            generateAnimationButtonInstance.transform.localScale = new Vector3(scaleResultScreen, scaleResultScreen);
            elapsed += Time.deltaTime;
            yield return null;
        }

        winMessagePanel.transform.localScale = endScale;
    }
    public void ShowWinMessage(bool didWhiteWin)
    {


        winMessagePanel.SetActive(true);
        winMessagePanel.transform.localScale = Vector3.zero;

        winsText = (didWhiteWin) ? whiteWinsText : blackWinsText;

        AnimateWinMessage(winMessagePanel.transform.position);
    }
    public void HideWinMessage()
    {
        winMessagePanel.SetActive(false);
    }

    public void PlayResignButtonEffect(Vector3 position)
    {
        StartCoroutine(ResignButtonEffectCoroutine(position));
    }
    public void PlayGenerateAnimationEffect(Vector3 position)
    {
        StartCoroutine(GenerateAnimationButtonEffectCoroutine(position));
    }
    private IEnumerator GenerateAnimationButtonEffectCoroutine(Vector3 position)
    {
        Vector3 resultScreenPos = transform.position + new Vector3(0.5f, 1f, -2f);
        Vector3 generateAnimationButtonPos = resultScreenPos + new Vector3(0f, -3.5f, -3f);
        Vector3 generateAnimationButtonBackgroundPos = generateAnimationButtonPos + new Vector3(0f, -1f);
        generateAnimationButtonBackgroundPos.z = -2f;
        buttonBackgroundInstance = Instantiate(generateAnimationButtonBackground, generateAnimationButtonBackgroundPos, Quaternion.identity);


        SpriteRenderer sr = buttonBackgroundInstance.GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;

        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);
            float scaleResultScreen = Map(t, 0f, 1f, 0f, 3f);
            float effectScale = Map(t, 0f, 1f, 3f, 4.5f);


            buttonBackgroundInstance.transform.localScale = new Vector3(effectScale, effectScale);

            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, t));

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator ResignButtonEffectCoroutine(Vector3 position)
    {
        GameObject effect = Instantiate(resignButtonBackground, position, Quaternion.identity);
        SpriteRenderer sr = effect.GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;

        float duration = 0.3f;
        float elapsed = 0f;
        float startScale = 1f;
        float endScale = 1.4f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float scale = Mathf.Lerp(startScale, endScale, t);
            effect.transform.localScale = new Vector3(scale, scale, 1f);

            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, t));

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(effect);
    }
    public bool HasGenerateAnimationPressed()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition);
        if (hit == null) return false;
        if (hit.gameObject.name == "GenerateAnimationButton_0(Clone)")
        {
            Debug.Log("Button hit");
            PlayGenerateAnimationEffect(hit.transform.position + new Vector3(0f, 0f, -2.1f));
            return true;
        }
        return false;
    }

    public bool HasButtonBeenPresed(bool isWhiteTurn)
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition);
        if (hit == null) return false;
        if (hit.gameObject.name == "ResignButtonBlack_0" && !isWhiteTurn)
        {
            PlayResignButtonEffect(hit.transform.position + new Vector3(0f, 0f, 1f));
            EndGame(!isWhiteTurn);
            return true;
        }
        if (hit.gameObject.name == "ResignButtonWhite_0" && isWhiteTurn)
        {
            PlayResignButtonEffect(hit.transform.position + new Vector3(0f, 0f, 1f));
            EndGame(!isWhiteTurn);
            return true;
        }
        return false;
    }

    public void EndGame(bool didWhiteWin)
    {
        ShowWinMessage(didWhiteWin);
    }

}

