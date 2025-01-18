using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioSource audioSource; // 音效播放器
    public AudioClip soundOnEnter; // 鼠标移入时的音效
    public AudioClip soundOnExit; // 鼠标移出时的音效
    public AudioClip soundOnClick; // 鼠标点击时的音效

    // 当鼠标移入按钮时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(soundOnEnter);
    }

    // 当鼠标移出按钮时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        audioSource.PlayOneShot(soundOnExit);
    }

    // 当鼠标点击按钮时调用
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(soundOnClick);
    }
}
