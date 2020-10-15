using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int RESTART_DELAY = 1;

    private void OnEnable()
    {
        Player.OnPlayerDied += OnPlayerDied;
        PortalZone.OnLevelComplite += OnVictory;
    }

    private void OnDisable()
    {
        Player.OnPlayerDied -= OnPlayerDied;
        PortalZone.OnLevelComplite -= OnVictory;
    }

    private void OnPlayerDied()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void OnVictory()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index >= 7)
            index = 1;

        StartCoroutine(LoadScene(index));
    }

    private IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(RESTART_DELAY);
        SceneManager.LoadScene(index);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
