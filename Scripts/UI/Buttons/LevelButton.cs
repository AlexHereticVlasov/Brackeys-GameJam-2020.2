using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : UIButton
{
    private const int LEVEL_OFFSET = 1;

    [SerializeField] private int _level = 0;
    [SerializeField] private TMP_Text _text = null;

    private void Start()
    {
        _text.text = _level.ToString();
    }

    public void OnClick()
    {
        SceneManager.LoadScene(_level + LEVEL_OFFSET);
    }
}
