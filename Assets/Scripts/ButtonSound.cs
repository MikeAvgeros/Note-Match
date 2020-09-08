using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerDownHandler
{
    public AudioClip buttonSound;
    private AudioPoolManager audioPoolManager;

    private void Start()
    {
        audioPoolManager = AudioPoolManager.instance;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        audioPoolManager.PlayUISound(buttonSound);
    }
}
