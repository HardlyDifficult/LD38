
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Sound
{
    public class MusicRotation : MonoBehaviour
    {
        public bool StartRotationOnAwake = true;
        public List<string> RotationSounds;

        private Queue<SoundClip> _backgroundMusicQueue = new Queue<SoundClip>();
        private SoundManager manager;
        private bool _isRunning = false;

        private void Start()
        {
            manager = Game.SoundManager;

            if (manager != null)
            {
                for (int i = 0; i < RotationSounds.Count; i++)
                {
                    SoundClip? clip = manager.GetSoundClip(RotationSounds[i]);

                    if (clip.HasValue)
                    {
                        _backgroundMusicQueue.Enqueue(clip.Value);
                    }
                }
            }

            if (StartRotationOnAwake)
                StartRotation();
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                StopRotation();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                StartRotation();
            }
        }

        //TODO: Running game at a higher time scale causes multiple songs to play :(
        IEnumerator CycleBackgroundSongs()
        {
            while (_isRunning)
            {
                if (_backgroundMusicQueue.Count > 0)
                {
                    SoundClip clip = _backgroundMusicQueue.Dequeue();

                    manager.PlayMusic(clip);

                    _backgroundMusicQueue.Enqueue(clip);

                    yield return new WaitForSeconds(clip.UnityClip.length);
                }
                else
                {
                    yield return  new WaitForEndOfFrame();
                }
            }
        }

        public void StopRotation()
        {
            if (_isRunning)
            {
                _isRunning = false;
                manager.StopMusic();
            }
        }

        public void StartRotation()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                StartCoroutine(CycleBackgroundSongs());
            }
        }


    }
}
