using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3
{
    public class SceneData : MonoBehaviour
    {
        public Transform CameraTransform;
        public Camera Camera;
        public UI UI;
        public Tilemap tileMap;
        public List<Tile> tiles = new List<Tile>();
    }
}