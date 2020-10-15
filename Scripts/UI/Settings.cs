using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    public const string FILE_NAME = "Settings";

    [Header("AudioMixers")]
    [SerializeField] private AudioMixer _audioMixer = null;
    [SerializeField] private AudioMixer _musicMixer = null;

    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown _qualityDropdown = null;
    [SerializeField] private TMP_Dropdown _resolutionDropdown = null;
    [SerializeField] private Slider _volumeSlider = null;
    [SerializeField] private Slider _musicSlider = null;

    private Resolution[] res;

    private SettingsData _data;

    private void Awake()
    {
        if (Saver.TryLoadData(FILE_NAME, out SettingsData data))
        {
            _data = data;
        }
        else
        {
            _data = new SettingsData();
            _data.Qulity = QualitySettings.GetQualityLevel();
            _data.Resolution = _resolutionDropdown.value;
            _data.Volume = _volumeSlider.value;
            _data.Music = _musicSlider.value;
        }
    }

    void Start()
    {
        AddQuality();
        AddResolutions();
        SetDefoultVolume("Volume", _volumeSlider, _audioMixer);
        SetDefoultMusic("Music", _musicSlider, _musicMixer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Application.isEditor)
            {
                Saver.DeleteFile(FILE_NAME);
                Debug.Log("File has deleted");
            }
        }
    }

    private void AddQuality()
    {
        _qualityDropdown.ClearOptions();
        _qualityDropdown.AddOptions(QualitySettings.names.ToList());

        if (_data.IsNewOne)
        {
            _qualityDropdown.value = _data.Qulity;
            QualitySettings.SetQualityLevel(_data.Qulity);
        }
        else
        {
            _qualityDropdown.value = QualitySettings.GetQualityLevel();
        }
    }

    private void AddResolutions()
    {
        _resolutionDropdown.ClearOptions();
        Resolution[] resolutions = Screen.resolutions;
        res = resolutions.Distinct().ToArray();
        string[] strRes = new string[res.Length];
        for (int i = 0; i < res.Length; i++)
        {
            strRes[i] = $"{res[i].width}x{res[i].height}";
        }
        _resolutionDropdown.AddOptions(strRes.ToList());


        if (_data.IsNewOne)
            Screen.SetResolution(res[_resolutionDropdown.value].width, res[_resolutionDropdown.value].height, Screen.fullScreen);
        else
            Screen.SetResolution(res[res.Length - 1].width, res[res.Length - 1].height, Screen.fullScreen);
        Debug.Log(_data.Resolution);
        _resolutionDropdown.value = _data.Resolution;
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(_qualityDropdown.value);
        _data.Qulity = _qualityDropdown.value;
        Save();
    }

    public void SetResolution()
    {
        Screen.SetResolution(res[_resolutionDropdown.value].width, res[_resolutionDropdown.value].height, Screen.fullScreen);
        _data.Resolution = _resolutionDropdown.value;
        Save();
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
        _data.Volume = volume;
        Save();
    }

    public void SetMusic(float volume)
    {
        _musicMixer.SetFloat("Music", volume);
        _data.Music = volume;
        Save();
    }

    private void SetDefoultVolume(string key, Slider slider, AudioMixer audioMixer)
    {
        if (_data != null)
        {
            slider.value = _data.Volume;
            audioMixer.SetFloat(key, _data.Volume);
        }
        else
        {
            audioMixer.SetFloat(key, slider.value);
        }
    }

    private void SetDefoultMusic(string key, Slider slider, AudioMixer audioMixer)
    {
        if (_data != null)
        {
            slider.value = _data.Music;
            audioMixer.SetFloat(key, _data.Music);
        }
        else
        {
            audioMixer.SetFloat(key, slider.value);
        }
    }

    private void Save()
    {
        _data.IsNewOne = false;
        Saver.SaveData(_data, FILE_NAME);
    }
}
