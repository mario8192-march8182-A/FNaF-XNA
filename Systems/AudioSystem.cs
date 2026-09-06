using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace FNaF_XNA.Systems
{
    /// <summary>
    /// Manages audio playback including music and sound effects.
    /// </summary>
    public class AudioSystem
    {
        private Dictionary<string, SoundEffect> soundEffects;
        private Dictionary<string, SoundEffectInstance> soundInstances;
        private SoundEffect backgroundMusic;
        private float masterVolume;

        public AudioSystem()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
            soundInstances = new Dictionary<string, SoundEffectInstance>();
            masterVolume = 1f;
        }

        public void Initialize()
        {
            // Initialize audio system
        }

        public void LoadContent(ContentManager content)
        {
            try
            {
                // Load sound effects
                // LoadSoundEffect("scary", content, "Sounds/scary");
                // LoadSoundEffect("jumpscare", content, "Sounds/jumpscare");
                // backgroundMusic = content.Load<SoundEffect>("Music/ambience");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio Load Error: {ex.Message}");
            }
        }

        public void LoadSoundEffect(string name, ContentManager content, string assetPath)
        {
            try
            {
                if (!soundEffects.ContainsKey(name))
                {
                    var sound = content.Load<SoundEffect>(assetPath);
                    soundEffects.Add(name, sound);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load sound {name}: {ex.Message}");
            }
        }

        public void PlaySoundEffect(string name, bool loop = false)
        {
            if (soundEffects.ContainsKey(name))
            {
                var instance = soundEffects[name].CreateInstance();
                instance.IsLooped = loop;
                instance.Volume = masterVolume;
                instance.Play();

                if (soundInstances.ContainsKey(name))
                {
                    soundInstances[name].Dispose();
                }
                soundInstances[name] = instance;
            }
        }

        public void PlayBackgroundMusic()
        {
            if (backgroundMusic != null)
            {
                var instance = backgroundMusic.CreateInstance();
                instance.IsLooped = true;
                instance.Volume = masterVolume * 0.5f;
                instance.Play();
            }
        }

        public void StopSound(string name)
        {
            if (soundInstances.ContainsKey(name))
            {
                soundInstances[name].Stop();
                soundInstances[name].Dispose();
                soundInstances.Remove(name);
            }
        }

        public void StopAllSounds()
        {
            foreach (var instance in soundInstances.Values)
            {
                instance.Stop();
                instance.Dispose();
            }
            soundInstances.Clear();
        }

        public void Update(GameTime gameTime)
        {
            // Update audio system
        }

        public void SetMasterVolume(float volume)
        {
            masterVolume = MathHelper.Clamp(volume, 0f, 1f);

            foreach (var instance in soundInstances.Values)
            {
                instance.Volume = masterVolume;
            }
        }

        public float MasterVolume => masterVolume;
    }
}
