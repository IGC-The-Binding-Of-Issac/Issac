using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageController : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Image image6;
    public Image image7;
    public Image image8;
    public Image image9;
    public Image image10;

    public float waitTime; // �̹����� �������� �����ϱ� �����ϱ������ ��� �ð� (��)
    public float fadeDuration; // �̹����� �������� 0�� �Ǵµ� �ɸ��� �ð� (��)

    void Start()
    {

        waitTime = 8;
        fadeDuration = 10;

        // 20�� �Ŀ� �̹����� �������� ���������� 0���� ����� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(WaitAndFadeOut(image1, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image2, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image3, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image4, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image5, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image6, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image7, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image8, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image9, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image10, waitTime, fadeDuration));

        Invoke("ReturnIntro", waitTime + fadeDuration + 2f);
    }

    IEnumerator WaitAndFadeOut(Image targetImage, float waitTime, float duration)
    {
        yield return new WaitForSeconds(waitTime);

        float startTime = Time.time;
        Color startColor = targetImage.color;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            targetImage.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 0, t));
            yield return null;
        }

        targetImage.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    void ReturnIntro()
    {
        SceneManager.LoadScene("01_Intro");
    }
}
