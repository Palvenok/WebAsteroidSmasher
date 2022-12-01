using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private float _defaultSFXVolume;
    private float _defaultMusicVolume;

    private float _muteValue = -80f;


    private void Awake()
    {
        _mixer.GetFloat("SFX", out _defaultSFXVolume);
        _mixer.GetFloat("Music", out _defaultMusicVolume);
    }

    public void MuteSFX(bool enabled)
    {
        _mixer.SetFloat("SFX", enabled ? _defaultSFXVolume : _muteValue);
    }
    public void MuteMusic(bool enabled)
    {
        _mixer.SetFloat("Music", enabled ? _defaultMusicVolume : _muteValue);
    }
}
