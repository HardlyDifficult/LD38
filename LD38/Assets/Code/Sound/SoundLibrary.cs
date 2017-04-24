using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public List<SoundClip> SoundClips;

    public SoundClip? GetByName(string name)
    {
        for (int i = 0; i < SoundClips.Count; i++)
        {
            if (SoundClips[i].Name == name)
                return SoundClips[i];
        }

        return null;
    }

}
