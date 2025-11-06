using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgMusic;
    void Initialize(AudioSource bgMusic)
    {
        this.bgMusic = bgMusic;
        bgMusic = GetComponent<AudioSource>();
        bgMusic.Play();
    }

    void Process()
    {

    }
    
    public void RaccoonNoise(string noiseType)
    {
        if (noiseType == "grumble")
        {
            //grumble.Play()
        }
        else if(noiseType == "chatter")
        {
            //chatter.Play()
            
        }
    }
}
