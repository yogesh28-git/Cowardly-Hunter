using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public SoundType[] soundTypes;

    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sfx;

    private void Awake()
    {
       if(Instance == null)
       {
           instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayMusic(Sounds.menuTheme);
        }
        else
        {
            PlayMusic(Sounds.gameTheme);
        }
    }
    public void PlayMusic(Sounds sound)
    {
        SoundType item = Array.Find(soundTypes, i => i.soundName == sound);
        AudioClip clip = item.audioclip;
        if (clip != null)
        {
            music.clip = clip;
            music.Play();
        }
        else
        {
            Debug.Log("Clip not found for sound:" + sound);
        }
    }
    public void PlayEffects(Sounds sound)
    {
        Debug.Log("Effects");
        SoundType item = Array.Find(soundTypes, i => i.soundName == sound);
        AudioClip clip = item.audioclip;
        if (clip != null)
        {
            sfx.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Clip not found for sound:" + sound);
        }
    }
}
[Serializable]
public class SoundType
{
    public Sounds soundName;
    public AudioClip audioclip;
}
public enum Sounds
{
    menuTheme,
    gameTheme,
    gameOver,
    danger,
    click,
    pickUp
}
