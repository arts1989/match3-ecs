using System.Collections;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = nameof(AudioConfig), menuName = "ScriptableObject/" + nameof(AudioConfig))]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip swipeSound;
        public AudioClip destroyBlockSound;
        public AudioClip spawnEventSound;
        
    }
}