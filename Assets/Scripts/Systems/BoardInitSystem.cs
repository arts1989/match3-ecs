using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Match3
{
    internal class BoardInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private GameConfig _gameConfig;
        private Board _board;

        public void Init() 
        {
            // система генерации ентитей доски - спавним префабы из конфига
            // и ставим геймобжекту ссылку на ентитю через компонент-монобех
            // сохраняем состояние доски по координатам, в них лежит айди ентити

             for (int x = 0; x < _board.Rows; x++)
             {
                for (int y = 0; y < _board.Columns; y++)
                {
                    var entity = _world.NewEntity();

                    int randomNum = Random.Range(0, _gameConfig.gems.Count);
                    var obj = Object.Instantiate(_gameConfig.gems[randomNum].sprite);

                    obj.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
                    obj.AddComponent<BoxCollider>();
                    obj.transform.position = new Vector3(
                        x + _gameConfig.offset.x * x,
                        y + _gameConfig.offset.y * y
                    );

                    entity.Get<Position>().value = new Vector2Int(x, y);
                    entity.Get<BlockType>().value = _gameConfig.gems[randomNum].type;
                    entity.Get<LinkToObject>().value = obj;

                    _board[x, y] = entity.GetInternalId(); 
                }
            }
        }
    }
}