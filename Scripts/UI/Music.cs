using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    private static Music _instanse;

    public AudioSource _audioSource;
    public AudioClip[] _tracks;

    public bool _isPaused = false;

    public int trackNumber = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instanse == null)
            _instanse = this;
        else
            Destroy(gameObject);

        if (_tracks.Length > 0 && !_isPaused)
            _audioSource.clip = _tracks[trackNumber];
        else
            Debug.Log("No Tracks in Playlist");

        StartCoroutine(StartPlay());
    }

    void Update()
    {
        if (!_audioSource.isPlaying && !_isPaused)
        {
            trackNumber = Random.Range(0, _tracks.Length);
            _audioSource.clip = _tracks[trackNumber];
            _audioSource.Play();
        }
    }

    private IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(.1f);
        trackNumber = 0;
        _audioSource.Play();
    }
}
