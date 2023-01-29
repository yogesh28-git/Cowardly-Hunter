using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    click,
    pickUp,
    gameOver,
    tigerKill,
    theme
}
public class AudioManager : MonoBehaviour
{
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private AudioClip SoundMap(Sounds sound)
    {
        AudioClip audioclip;
        switch (sound)
        {
            case Sounds.click:
                break;
            case Sounds.gameOver:
                break;
            case Sounds.tigerKill:
                break;
            case Sounds.pickUp:
                break;
            case Sounds.theme:
                break;
        }
        return audioclip;
    }
}
