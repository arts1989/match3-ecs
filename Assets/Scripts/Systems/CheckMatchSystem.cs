using Leopotam.Ecs;
 namespace Match3
{
    internal class CheckMatchSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMatchEvent, LinkToObject, Position, BlockType> _filter;
        private GameState _gameState;
        private Configuration _configuration;

        public void Run()
        {
            //система помечает какие ентити уничтожить
            foreach (var index in _filter)
            {
                ref var position = ref _filter.Get3(index).value;
                var board = _gameState.Board;

                foreach (var coords in board.getMatchCoords(position, _configuration.minChainLenght))
                {
                    board[coords].Get<DestroyEvent>();
                }

                break; //отладить проверку линий куда пришел кубик сосед, глючит
            }
        }
    }
}