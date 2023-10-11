using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Match3
{
    public class SceneData : MonoBehaviour
    {
        public Transform CameraTransform;
        public Camera Camera;
        public UI UI;
        public Image background;
        public Tilemap tileMap;
        public List<Tile> tiles = new List<Tile>();
        public AudioSource backgroundMusic;
        public AudioSource blocksAudio;
    }
}