using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgMusic;
    private AudioSource raccoonGrumble;
    private AudioSource raccoonChatter;
    private AudioSource waterFallSound;
    private AudioMixerSnapshot snapshotNormal;
    private AudioMixerSnapshot snapshotWaterFall;
    private float transitionTime = 0.2f;

    public void Initialize(AudioSource bgMusic, AudioSource raccoonGrumble, AudioSource raccoonChatter, AudioSource waterFallSound, AudioMixerSnapshot snapshotWaterFall, AudioMixerSnapshot snapshotNormal)
    {
        this.bgMusic = bgMusic;
        this.raccoonGrumble = raccoonGrumble;
        this.raccoonChatter = raccoonChatter;
        this.waterFallSound = waterFallSound;
        this.snapshotNormal = snapshotNormal;
        this.snapshotWaterFall = snapshotWaterFall;
        // bgMusic.Play();
    }

    public void Process()
    {

    }

    public void RaccoonNoise(string noiseType)
    {
        if (noiseType == "grumble")
        {
            raccoonGrumble.Play();
        }
        else if (noiseType == "chatter")
        {
            raccoonChatter.Play();

        }
    }
    
    
    public void WaterFallSound(bool playerIsClose)
    {
        if (playerIsClose)
        {

            snapshotWaterFall.TransitionTo(transitionTime);
        }
        else
        {
            snapshotNormal.TransitionTo(transitionTime);            
        }
    }

    
}
