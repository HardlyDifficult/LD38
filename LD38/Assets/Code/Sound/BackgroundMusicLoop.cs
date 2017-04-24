
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Sound
{
    public class BackgroundMusicLoop : MonoBehaviour
    {
        private Queue<SoundClip> _backgroundMusicQueue = new Queue<SoundClip>();
        private SoundManager manager;

        private void Start()
        {
            manager = Game.SoundManager;

            if (manager != null)
            {
                //TODO: Probably should check if these have a value
                SoundClip bgm1 = manager.GetSoundClip("bgm1").Value;
                SoundClip bgm2 = manager.GetSoundClip("bgm2").Value;
                SoundClip bgm3 = manager.GetSoundClip("bgm3").Value;

                _backgroundMusicQueue.Enqueue(bgm1);
                _backgroundMusicQueue.Enqueue(bgm2);
                _backgroundMusicQueue.Enqueue(bgm3);
            }

            StartCoroutine(CycleBackgroundSongs());
        }

        //TODO: Running game at a higher time scale causes multiple songs to play :(
        IEnumerator CycleBackgroundSongs()
        {
            while (true)
            {
                SoundClip clip = _backgroundMusicQueue.Dequeue();

                manager.PlayMusic(clip);

                _backgroundMusicQueue.Enqueue(clip);

                yield return new WaitForSeconds(clip.UnityClip.length);
            }
        }



    }
}
