using Leopotam.Ecs;
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
        } 
    }
}