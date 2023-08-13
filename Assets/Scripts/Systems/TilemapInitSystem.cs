using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class TilemapInitSystem : IEcsInitSystem
    {
        private GameState _gameState;
        private SceneData _sceneData;

        public void Init()
        {
            var tilemap = _sceneData.tileMap;
            var tiles = _sceneData.tiles;
            var underlayTiles = _sceneData.underlayTiles;

            //подложка
            for (int x = 0; x < _gameState.Columns; x++)
            {
                for (int y = 0; y < _gameState.Rows; y++)
                {
                    var coord = new Vector3Int(x, y);
                    tilemap.SetTile(coord, tiles[0]);
                }
            }

            //покрытия
            var underlayCount = _gameState.UnderlayCount;
            while (underlayCount > 0)
            {
                var coord = new Vector3Int(
                    Random.Range(0, _gameState.Columns - 1),
                    Random.Range(0, _gameState.Rows - 1)
                );

                tilemap.SetTile(coord, underlayTiles[0]);
                underlayCount--;
            }

            //бордеры
            for (int x = 0; x < _gameState.Columns; x++)
            {
                var coord = new Vector3Int(x, -1);
                tilemap.SetTile(coord, tiles[1]);
                tilemap.SetTransformMatrix(coord, Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90f)));

            }

            for (int y = 0; y < _gameState.Rows; y++)
            {
                var coord = new Vector3Int(-1, y);
                tilemap.SetTile(coord, tiles[1]);
                tilemap.SetTransformMatrix(coord, Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180f)));

            }

            for (int x = 0; x < _gameState.Columns; x++)
            {
                var coord = new Vector3Int(x, _gameState.Rows);
                tilemap.SetTile(coord, tiles[1]);
                tilemap.SetTransformMatrix(coord, Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90f)));
            }

            for (int y = 0; y < _gameState.Rows; y++)
            {
                var coord = new Vector3Int(_gameState.Columns, y);
                tilemap.SetTile(coord, tiles[1]);
            }

            //углы
            var bottomLeft = new Vector3Int(-1, -1);
            var topLeft = new Vector3Int(-1, _gameState.Rows);
            var topRight = new Vector3Int(_gameState.Rows, _gameState.Columns);
            var bottomRight = new Vector3Int(_gameState.Columns, -1);

            tilemap.SetTile(bottomLeft, tiles[2]);
            tilemap.SetTile(topLeft, tiles[3]);
            tilemap.SetTile(topRight, tiles[4]);
            tilemap.SetTile(bottomRight, tiles[5]);
        }
    }
}