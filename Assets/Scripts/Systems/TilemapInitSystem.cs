using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3
{
    internal class TilemapInitSystem : IEcsInitSystem
    {
        private GameState _gameState;
        private SceneData _sceneData;
        private Configuration _configuration;

        public void Init()
        {
            var tilemap = _sceneData.tileMap;
            var tiles = _sceneData.tiles;
            var underlays = _configuration.underlays;

            if (_gameState.blockPositionsActivated)
            {
                foreach (var row in _gameState.blockPositions)
                {
                    var coord = (Vector3Int) row.Key;
                    tilemap.SetTile(coord, tiles[0]);
                }

                foreach (var row in _gameState.underlayPositions)
                {
                    var coord = (Vector3Int) row.Key;
                    tilemap.SetTile(coord, underlays[0].tiles[0]);
                }
            }
            else
            {
                //подложка
                for (int x = 0; x < _gameState.Columns; x++)
                {
                    for (int y = 0; y < _gameState.Rows; y++)
                    {
                        var coord = new Vector3Int(x, y);

                        if (_gameState.emptyPositionsActivated)
                            if (_gameState.emptyPositions.Contains((Vector2Int)coord))
                                continue;

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

                    if (_gameState.emptyPositionsActivated)
                        if (_gameState.emptyPositions.Contains((Vector2Int)coord))
                            continue;

                    tilemap.SetTile(coord, underlays[0].tiles[0]);
                    underlayCount--;
                }
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