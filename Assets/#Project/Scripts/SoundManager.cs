using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgMusic;
    private AudioSource raccoonGrumble;
    private AudioSource raccoonChatter;
    private AudioSource rummageSound;
    private AudioSource opossumSound;
    private AudioSource waterFallSound;
    private AudioMixer audioMixer;
    private AudioMixerSnapshot snapshotNormal;
    private AudioMixerSnapshot snapshotWaterFall;
    private float transitionTime = 0.2f;
   private float maxDistance = 10f;
    private AnimationCurve animationCurve;
    public void Initialize(AudioSource bgMusic, AudioSource raccoonGrumble, AudioSource raccoonChatter, AudioSource rummageSound, AudioSource opossumSound, AudioSource waterFallSound, AudioMixerSnapshot snapshotWaterFall, AudioMixerSnapshot snapshotNormal, AudioMixer audioMixer, AnimationCurve animationCurve)
    {
        this.bgMusic = bgMusic;
        this.raccoonGrumble = raccoonGrumble;
        this.raccoonChatter = raccoonChatter;
        this.rummageSound = rummageSound;
        this.opossumSound = opossumSound;
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

    public void OpossumNoise(string noiseType)
    {
        if (noiseType == "lucy")
        {
            opossumSound.Play();
        }
        else if (noiseType == "baby")
        {
            opossumSound.Play();

        }
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

    public void rummageSoundPlay()
    {
        rummageSound.Play();
    }


    public void WaterFallSound(Transform player)
    {
        float distance = Vector3.Distance(player.position, waterFallSound.transform.position);
        // Debug.Log(waterFallSound.transform.position);
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
