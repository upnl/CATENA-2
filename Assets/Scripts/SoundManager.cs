using UnityEngine;
using System;

public enum SoundType //넣을 소리 종류
{
    Swing,
    Tag,
    SwordSkill,
    ShieldSkill,
    PunchSkill,
    Hit,
    Stun,
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    public static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType sound, float volume = 1) //이거 쓰면 됨 ^^
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);

        //instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
            soundList[i].name = names[i];
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds
    {
        get => sounds;
    }
    
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}