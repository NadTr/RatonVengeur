using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgMusic;
    private AudioSource raccoonGrumble;
    private AudioSource raccoonChatter;
    private AudioSource waterFallSound;
    private AudioMixer audioMixer;
    private AudioMixerSnapshot snapshotNormal;
    private AudioMixerSnapshot snapshotWaterFall;
    private float transitionTime = 0.2f;
   private float maxDistance = 10f;
    private AnimationCurve animationCurve;
    public void Initialize(AudioSource bgMusic, AudioSource raccoonGrumble, AudioSource raccoonChatter, AudioSource waterFallSound, AudioMixerSnapshot snapshotWaterFall, AudioMixerSnapshot snapshotNormal, AudioMixer audioMixer, AnimationCurve animationCurve)
    {
        this.bgMusic = bgMusic;
        this.raccoonGrumble = raccoonGrumble;
        this.raccoonChatter = raccoonChatter;
        this.waterFallSound = waterFallSound;
        this.snapshotNormal = snapshotNormal;
        this.snapshotWaterFall = snapshotWaterFall;
        this.audioMixer = audioMixer;
        this.animationCurve = animationCurve;
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


    public void WaterFallSound(Transform player)
    {
        float distance = Vector3.Distance(player.position, waterFallSound.transform.position);
        Debug.Log(waterFallSound.transform.position);
        // float freq = Mathf.Lerp(22000, 10, distance / maxDistance);
        float freq = animationCurve.Evaluate(distance  / maxDistance);
        audioMixer.SetFloat("LowPassCutOff", freq);    
    }

    public void WaterFallSoundBool(bool playerIsClose)
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
