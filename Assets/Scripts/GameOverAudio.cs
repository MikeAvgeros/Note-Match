using System.Collections;
using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    public AudioClip bestScoreAudio;
    public AudioClip lowScoreAudio;
    public AudioClip zeroScoreAudio;
    public AudioPoolManager audioPoolManager;
    public GameManager gameManager;

    private void OnEnable()
    {
        StartCoroutine(CheckAudio());
    }

    private IEnumerator CheckAudio()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameManager.canPlayGOAudio == true)
        {
            if (gameManager.newBestScore == true)
            {
                audioPoolManager.PlayUISound(bestScoreAudio);
            }
            else if (gameManager.newBestScore == false && gameManager.currentScore > 0)
            {
                audioPoolManager.PlayUISound(lowScoreAudio);
            }
            else
            {
                audioPoolManager.PlayUISound(zeroScoreAudio);
            }
        }
    }
}
