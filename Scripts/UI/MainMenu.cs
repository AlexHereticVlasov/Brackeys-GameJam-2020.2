using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels = null;

    public void OpenPanel(GameObject panelToOpen)
    {
        foreach (var panel in _panels)
        {
            if (panel == panelToOpen)
                panel.SetActive(true);
            else
                panel.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
