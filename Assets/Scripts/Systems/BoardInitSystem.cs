using Leopotam.Ecs;
using System.Linq;
using UnityEngine;

namespace Match3
{
    internal class BoardInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Configuration _configuration;
        private GameState _gameState;

        public void Init() 
        {
            for (int x = 0; x < _gameState.Columns; x++)
            {
                for (int y = 0; y < _gameState.Rows; y++)
                {
                    if(_configuration.levels[_gameState.currentLevel].PrecipiceProperty.Contains
                        (new Vector2Int(x, y))) continue;
                    
                    var entity   = _world.NewEntity();
                    var position = new Vector2Int(x, y);

                    int randomNum = Random.Range(0, _configuration.blocks.Count);

                    var obj = _world.spawnGameObject(
                        position,
                        entity,
                        _configuration.blocks[randomNum].prefab,
                        _configuration.blocks[randomNum].sprites[0]
                    );

                    entity.Get<Position>().value = position;
                    entity.Get<BlockType>().value = _configuration.blocks[randomNum].type;
                    entity.Get<Points>().value = _configuration.blocks[randomNum].points;  
                    entity.Get<LinkToObject>().value = obj; //link to entity from gameobject

                    _gameState.Board[position] = entity;
                }
            }

            var obstacleCount = _gameState.ObstacleCount;

            while(obstacleCount > 0)
            {
                var index = Random.Range(0, _gameState.Board.Count);

                var position = _gameState.Board.Keys.ElementAt(index);
                var entity = _gameState.Board.Values.ElementAt(index);

                Object.Destroy(entity.Get<LinkToObject>().value); 
                entity.Destroy();

                entity = _world.NewEntity();

                var obj = _world.spawnGameObject(
                    position,
                    entity,
                    _configuration.obstacles[0].prefab,
                    _configuration.obstacles[0].sprites[0]
                );

                entity.Get<Position>().value = position;
                entity.Get<BlockType>().value = _configuration.obstacles[0].type;
                entity.Get<Points>().value = _configuration.obstacles[0].points;
                entity.Get<LinkToObject>().value = obj; //link to entity from gameobject
                entity.Get<Health>().value = _configuration.obstacles[0].health;

                _gameState.Board[position] = entity;

                obstacleCount--;
            }
        } 
    }
}