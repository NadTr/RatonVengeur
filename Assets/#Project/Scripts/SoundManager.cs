using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgMusic;
    private AudioSource raccoonGrumble;
    private AudioSource raccoonChatter;

    public void Initialize(AudioSource bgMusic, AudioSource raccoonGrumble, AudioSource raccoonChatter)
    {
        this.bgMusic = bgMusic;
        this.raccoonGrumble = raccoonGrumble;
        this.raccoonChatter = raccoonChatter;
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
        else if(noiseType == "chatter")
        {
            raccoonChatter.Play();
            
        }
    }
}
