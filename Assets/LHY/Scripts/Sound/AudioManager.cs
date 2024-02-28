using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

//AudioMixer에 관한 Control과 volume의 관리
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance;
    private void Awake()
    {
        //todo : main아닌 다른 씬에도 필요할지?
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxCource;
    //todo : 분리
    public AudioClip mainBgmClip;
    public AudioClip introBgmClip;
    public AudioClip walkClip;
    public AudioClip jumpClip;
    public AudioClip getItemClip;
    public AudioClip uiSelectClip;

    [SerializeField] private AudioMixer audioMixer;

    private readonly Dictionary<string, float> mixerVolume = new();
    private readonly Dictionary<string, bool> mixerMuteToggle = new();

    //public float bgmVolumeScale, sfxVolumeScale, masterVolumeScale;
    public void PlaySFX(AudioClip clip)
    {
        if (clip == walkClip && sfxCource.isPlaying)
            return;
        sfxCource.PlayOneShot(clip);

    }
    private void Start()
    {
        bgmSource.clip = mainBgmClip;

        bgmSource.Play();
        //SettingsSoundData();
    }
    private void Update()
    {
        //
    }

    /*
     *방식 변경 추후 삭제
    public void SettingsSoundData()
    {
        bgmVolumeScale = PlayerPrefs.GetFloat("BGMVolume");
        sfxVolumeScale = PlayerPrefs.GetFloat("SFXVolume");
        masterVolumeScale = PlayerPrefs.GetFloat("MasterVolume");
    }
    */
    public void ToggleVolume(string exposedParam, bool isToggledOn)
    {

        if (isToggledOn)
        {
            if (!mixerVolume.ContainsKey(exposedParam))
            {
                return;
            }

            float volume = mixerVolume[exposedParam];
            audioMixer.SetFloat(exposedParam, volume);
            PlayerPrefs.SetFloat(exposedParam, volume);
        }
        else
        {
            audioMixer.GetFloat(exposedParam, out float temp);
            mixerVolume[exposedParam] = temp;
            audioMixer.SetFloat(exposedParam, -80f);
            //todo : playerprefs의 KEY:Master의 slider value 덮어쓰게되서 
            //toggle의 key를 만들던가 playerprefs의 bool을 저장할 방법같은걸 찾아야함
            //PlayerPrefs.SetFloat(exposedParam, 0f);
        }
    }
    public void SetVolume(string mixerParam, float volume)
    {
        if (!mixerMuteToggle.ContainsKey(mixerParam))
        {
            mixerMuteToggle[mixerParam] = true;
        }

        bool isToggledOn = mixerMuteToggle[mixerParam];
        if (isToggledOn)
        {
            float result = CalculateMixerVolume(volume);
            audioMixer.SetFloat(mixerParam, result);
            PlayerPrefs.SetFloat(mixerParam, volume);
            Debug.Log("AudioDataSet");
        }
        else
        {
            mixerVolume[mixerParam] = CalculateMixerVolume(volume);
        }

        //소리 표준화(slider = 1 ~ 0dB, audiomixergroup = 0 ~ -50dB)
        float CalculateMixerVolume(float f)
        {

            return -Mathf.Pow(51f, 1f - f) + 1f;
        }

    }

}
