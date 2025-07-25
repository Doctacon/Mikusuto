using System.Collections.Generic;
using UnityEngine;

namespace Mikusuto.Systems
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance => instance;
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource ambientSource;
        
        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] musicTracks;
        [SerializeField] private AudioClip[] footstepSounds;
        [SerializeField] private AudioClip interactSound;
        [SerializeField] private AudioClip dialogueSound;
        
        [Header("Volume Settings")]
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 0.7f;
        [Range(0f, 1f)] public float sfxVolume = 1f;
        [Range(0f, 1f)] public float ambientVolume = 0.5f;
        
        private Dictionary<string, AudioClip> audioClips;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeAudioClips();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void InitializeAudioClips()
        {
            audioClips = new Dictionary<string, AudioClip>();
        }
        
        public void PlayMusic(int trackIndex)
        {
            if (trackIndex < 0 || trackIndex >= musicTracks.Length) return;
            
            musicSource.clip = musicTracks[trackIndex];
            musicSource.volume = musicVolume * masterVolume;
            musicSource.Play();
        }
        
        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;
            sfxSource.PlayOneShot(clip, sfxVolume * masterVolume);
        }
        
        public void PlayFootstep()
        {
            if (footstepSounds.Length == 0) return;
            
            int randomIndex = Random.Range(0, footstepSounds.Length);
            PlaySFX(footstepSounds[randomIndex]);
        }
        
        public void PlayInteractSound()
        {
            PlaySFX(interactSound);
        }
        
        public void PlayDialogueSound()
        {
            PlaySFX(dialogueSound);
        }
        
        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            musicSource.volume = musicVolume * masterVolume;
        }
        
        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
        }
        
        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);
            UpdateAllVolumes();
        }
        
        void UpdateAllVolumes()
        {
            musicSource.volume = musicVolume * masterVolume;
            sfxSource.volume = sfxVolume * masterVolume;
            ambientSource.volume = ambientVolume * masterVolume;
        }
    }
}