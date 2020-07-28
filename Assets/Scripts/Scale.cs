using UnityEngine;
using UnityEngine.EventSystems;

public class Scale : MonoBehaviour, IPointerDownHandler
{
    public string scale;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.scale = scale;
    }
}
