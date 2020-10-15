using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _topPanel = null;
    [SerializeField] private GameObject _pausePanel = null;
    [SerializeField] private KeyCode _escape = KeyCode.Escape;

    private void Update()
    {
        if (Input.GetKeyDown(_escape))
            SetPanelActive();
    }

    public void SetPanelActive()
    {
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        _topPanel.SetActive(!_pausePanel.activeSelf);

        if (_pausePanel.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
