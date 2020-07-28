using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IPointerDownHandler
{
    public int level;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.UpdateLevel(level);
    }
}
