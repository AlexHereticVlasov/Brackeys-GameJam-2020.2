using UnityEngine;

[System.Serializable]
public class SettingsData
{
    [SerializeField] private int _qulity;
    [SerializeField] private int _resolution;
    [SerializeField] private float _volume;
    [SerializeField] private float _music;

    public int Qulity { get { return _qulity; } set { _qulity = value; } }
    public int Resolution { get { return _resolution; } set { _resolution = value; } }
    public float Volume { get { return _volume; } set { _volume = value; } }
    public float Music { get { return _music; } set { _music = value; } }

    public bool IsNewOne { get; set; } = true;

    public SettingsData()
    {
        IsNewOne = true;
        Volume = -15;
        Music = -15;
    }
}
