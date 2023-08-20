using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3
{
    internal class ClearUnderlaySystem : IEcsRunSystem
    {
        private EcsFilter<SpawnEvent, Position> _filter;
        private GameState _gameState;
        private Configuration _configuration;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (int index in _filter)
            {
                ref var position = ref _filter.Get2(index).value;

                var coordTile = new Vector3Int(position.x, position.y);
                TileBase tile = _sceneData.tileMap.GetTile(coordTile);

                if (tile == _configuration.underlays[0].tiles[0])
                {
                    _gameState.PointsScored += _configuration.underlays[0].points;
                    _sceneData.tileMap.SetTile(coordTile, _sceneData.tiles[0]);
                }
            }
        }
    }
}