using UnityEngine;

public class Fade : MonoBehaviour
{
    private const string FADE = "Fade";

    [SerializeField] private Animator _animator = null;

    private void OnEnable()
    {
        Player.OnPlayerDied += OnLevelComplite;
        PortalZone.OnLevelComplite += OnLevelComplite;
    }

    private void OnDisable()
    {
        Player.OnPlayerDied -= OnLevelComplite;
        PortalZone.OnLevelComplite -= OnLevelComplite;
    }

    private void OnLevelComplite()
    {
        _animator.SetTrigger(FADE);
    }
}
