using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource = null;

    [Header("Sounds")] 
    [SerializeField] private AudioClip _button = null;

    private void OnEnable()
    {
        UIButton.OnEnter += OnButtonEnter;
    }

    private void OnDisable()
    {
        UIButton.OnEnter -= OnButtonEnter;
    }

    private void OnButtonEnter()
    {
        _audioSource.PlayOneShot(_button);
    }
}
