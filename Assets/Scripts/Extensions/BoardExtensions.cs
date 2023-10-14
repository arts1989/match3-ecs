using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public static class BoardExtensions
    {
        static private List<Vector2Int> _directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };
        static private int _chainLenght = 3;

        static private Dictionary<string, BlockTypes> _boosterMergeConfig = new Dictionary<string, BlockTypes>
        {
            { BlockTypes.DestroyLineHorizontal.GetHashCode() + "+" + BlockTypes.DestroyLineHorizontal.GetHashCode() ,  BlockTypes.DestroyCross },
            { BlockTypes.DestroyLineHorizontal.GetHashCode() + "+" + BlockTypes.DestroyLineVertical.GetHashCode() ,  BlockTypes.DestroyCross },
            { BlockTypes.DestroyLineVertical.GetHashCode() + "+" + BlockTypes.DestroyLineVertical.GetHashCode() ,  BlockTypes.DestroyCross },
            { BlockTypes.DestroyCross.GetHashCode() + "+" + BlockTypes.DestroyCross.GetHashCode() ,  BlockTypes.DestroySameType },
            { BlockTypes.DestroyLineHorizontal.GetHashCode() + "+" + BlockTypes.Homing.GetHashCode() ,  BlockTypes.HomingLineHorizontal },
            { BlockTypes.DestroyLineVertical.GetHashCode() + "+" + BlockTypes.Homing.GetHashCode() ,  BlockTypes.HomingLineVertical },
            { BlockTypes.BombSmall.GetHashCode() + "+" + BlockTypes.BombSmall.GetHashCode() ,  BlockTypes.BombBig },
            { BlockTypes.Homing.GetHashCode() + "+" + BlockTypes.Homing.GetHashCode() ,  BlockTypes.MultyHoming },
            { BlockTypes.BombSmall.GetHashCode() + "+" + BlockTypes.DestroyLineVertical.GetHashCode() ,  BlockTypes.BombSmallLineVertical },
        };

        public static (List<Vector2Int> coords, BlockTypes blockType) getMatchCoords(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position, ref Vector2Int oldPosition)
        {
            var matchCoords = new List<Vector2Int>();
            var coords = new List<Vector2Int>();

            bool hasDestroyLineHorizontal = false; // match 5
            bool hasBombSmall = false; // match 4 (square)
            bool hasDestroyCross = false; // match 5 (Tetris - T)

            // провеяем матч 3 в ряд
            foreach (var direction in _directions)
            {
                if (direction + position == oldPosition)
                    continue; // пришли отсюда, не проверям, там гем другого типа

                var chainLenght = 1;
                var startPos = position;
                coords.Clear();

                if (direction != (oldPosition - position) && board.ContainsKey(position - direction)) // зацепим -1 кординату для проверки случая "двигаю между двумя одинакового типа"
                {
                    startPos -= direction;
                }

                while (board.TryGetValue(startPos, out var entity))
                {
                    var nextPos = startPos + direction;
                    if (board.checkBlocksSameType(ref startPos, ref nextPos))
                    {
                        coords.Add(startPos);
                        chainLenght++;
                    }
                    else if (chainLenght >= _chainLenght - 1)
                    {
                        break;
                    }

                    startPos += direction;
                }

                if (coords.Count >= _chainLenght - 1)
                {
                    var lastBlockInChainPos = coords[coords.Count - 1] + direction;
                    coords.Add(lastBlockInChainPos);

                    // тетрис _|_ - зацепим отросток если он есть и обрежем линию больше трех если он (отросток) есть
                    if (coords.Count >= 3)
                    {
                        var coordToCheckNearby = Vector2Int.zero;
                        var coordToCheckNearbyNext = Vector2Int.zero;
                        var coord1 = coords[1];

                        if (direction == Vector2Int.left || direction == Vector2Int.right)
                        {
                            coordToCheckNearby = coords[1] + Vector2Int.down;
                            coordToCheckNearbyNext = coords[1] + Vector2Int.down + Vector2Int.down;

                            if (board.checkBlocksSameType(ref coord1, ref coordToCheckNearby, ref coordToCheckNearbyNext))
                            {
                                if (!matchCoords.Contains(coordToCheckNearby))
                                {
                                    matchCoords.Add(coordToCheckNearby);
                                }

                                if (!matchCoords.Contains(coordToCheckNearbyNext))
                                {
                                    matchCoords.Add(coordToCheckNearbyNext);
                                }

                                hasDestroyCross = true;
                            }

                            coordToCheckNearby = coords[1] + Vector2Int.up;
                            coordToCheckNearbyNext = coords[1] + Vector2Int.up + Vector2Int.up;

                            if (board.checkBlocksSameType(ref coord1, ref coordToCheckNearby, ref coordToCheckNearbyNext))
                            {
                                if (!matchCoords.Contains(coordToCheckNearby))
                                {
                                    matchCoords.Add(coordToCheckNearby);
                                }

                                if (!matchCoords.Contains(coordToCheckNearbyNext))
                                {
                                    matchCoords.Add(coordToCheckNearbyNext);
                                }

                                hasDestroyCross = true;
                            }
                        }
                        else if (direction == Vector2Int.down || direction == Vector2Int.up)
                        {
                            coordToCheckNearby = coords[1] + Vector2Int.left;
                            coordToCheckNearbyNext = coords[1] + Vector2Int.left + Vector2Int.left;

                            if (board.checkBlocksSameType(ref coord1, ref coordToCheckNearby, ref coordToCheckNearbyNext))
                            {
                                if (!matchCoords.Contains(coordToCheckNearby))
                                {
                                    matchCoords.Add(coordToCheckNearby);
                                }

                                if (!matchCoords.Contains(coordToCheckNearbyNext))
                                {
                                    matchCoords.Add(coordToCheckNearbyNext);
                                }

                                hasDestroyCross = true;
                            }

                            coordToCheckNearby = coords[1] + Vector2Int.right;
                            coordToCheckNearbyNext = coords[1] + Vector2Int.right + Vector2Int.right;

                            if (board.checkBlocksSameType(ref coord1, ref coordToCheckNearby, ref coordToCheckNearbyNext))
                            {
                                if (!matchCoords.Contains(coordToCheckNearby))
                                {
                                    matchCoords.Add(coordToCheckNearby);
                                }

                                if (!matchCoords.Contains(coordToCheckNearbyNext))
                                {
                                    matchCoords.Add(coordToCheckNearbyNext);
                                }

                                hasDestroyCross = true;
                            }
                        }
                    }

                    foreach (var coord in coords)
                    {
                        if (!matchCoords.Contains(coord))
                        {
                            matchCoords.Add(coord);
                        }
                    }
                }
            }

            if (matchCoords.Count >= 5 && hasDestroyCross == false) // боллее 5 в ряд или 2 матч3 уголком
            {
                hasDestroyLineHorizontal = true;
            }

            // провеяем кобминацию квадрат из 4
            var swipeDirection = position - oldPosition;
            foreach (var direction in _directions)
            {
                if (direction + position == oldPosition || position - direction == oldPosition)
                    continue; // убираем направления свайпа и обратку

                var pos1 = position;
                var pos2 = position + direction;
                var pos3 = swipeDirection + position;
                var pos4 = swipeDirection + position + direction;

                if (board.checkBlocksSameType(ref pos1, ref pos2, ref pos3, ref pos4))
                {
                    if (!matchCoords.Contains(pos1))
                    {
                        matchCoords.Add(pos1);
                    }

                    if (!matchCoords.Contains(pos2))
                    {
                        matchCoords.Add(pos2);
                    }

                    if (!matchCoords.Contains(pos3))
                    {
                        matchCoords.Add(pos3);
                    }

                    if (!matchCoords.Contains(pos4))
                    {
                        matchCoords.Add(pos4);
                    }

                    hasBombSmall = true;
                }
            }

            if (matchCoords.Count < _chainLenght)
                matchCoords.Clear();

            var boosterTypeToSpawnOnCurrentPosition = hasDestroyCross ? BlockTypes.DestroyCross
                : hasBombSmall ? BlockTypes.BombSmall
                : hasDestroyLineHorizontal ? BlockTypes.DestroyLineHorizontal
                : BlockTypes.Default;

            return (matchCoords, boosterTypeToSpawnOnCurrentPosition);
        }

        public static bool checkMoveAvaliable(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position, ref Vector2Int swipeDirection)
        {
            if (!board.ContainsKey(position + swipeDirection)) // свайп за пределы доски
                return false;

            var currentBlockType = board[position].Get<BlockType>();
            var swappingBlockType = board[position + swipeDirection].Get<BlockType>();

            if (currentBlockType.isObstacle || swappingBlockType.isObstacle)
            {
                //Debug.Log("ящики не двигаем, с ящиками не свапаемся");
                return false;
            }

            if(currentBlockType.isBooster)
            {
                return true; // разрешаем двигать бустеры
            }

            // провеяем матч 3 в ряд
            foreach (var direction in _directions)
            {
                if (direction + swipeDirection == Vector2.zero)
                    continue; // убираем обратное направление свайпа, там не может быть комбинации

                var chainLenght = 1;
                var startPos = position + swipeDirection;
                var coordToCheck = 2;

                if (direction != swipeDirection && board.ContainsKey(startPos - direction)) // зацепим -1 кординату для проверки случая "двигаю между двумя одинакового типа"
                {
                    startPos -= direction;
                    coordToCheck++;
                }

                while (board.TryGetValue(startPos, out var entity))
                {
                    var posToCheck = (startPos == position + swipeDirection) ? position : startPos;
                    var nextPosToCheck = (startPos + direction == position + swipeDirection) ? position : startPos + direction;

                    if (board.checkBlocksSameType(ref posToCheck, ref nextPosToCheck))
                    {
                        chainLenght++;
                    }

                    startPos += direction;
                    coordToCheck--;

                    if (chainLenght == _chainLenght)
                        return true;

                    if (coordToCheck == 0)
                        break;
                }
            }

            // провеяем кобминацию квадрат из 4
            foreach (var direction in _directions)
            {
                if (direction + swipeDirection == Vector2.zero || direction == swipeDirection)
                    continue; // убираем направления свайпа и обратку

                var startPos = position + swipeDirection;

                var pos1 = position;
                var pos2 = startPos + direction;
                var pos3 = swipeDirection + startPos;
                var pos4 = swipeDirection + startPos + direction;

                if (board.checkBlocksSameType(ref pos1, ref pos2, ref pos3, ref pos4))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool checkBlocksSameType(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int pos1, ref Vector2Int pos2)
        {
            if (board.ContainsKey(pos1) && board.ContainsKey(pos2))
                return board[pos1].Get<BlockType>().value == board[pos2].Get<BlockType>().value ? true : false;

            return false;
        }

        public static bool checkBlocksSameType(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int pos1, ref Vector2Int pos2, ref Vector2Int pos3)
        {
            if (board.ContainsKey(pos1) && board.ContainsKey(pos2) && board.ContainsKey(pos3))
                return (board[pos1].Get<BlockType>().value == board[pos2].Get<BlockType>().value
                    && board[pos1].Get<BlockType>().value == board[pos3].Get<BlockType>().value) ? true : false;

            return false;
        }

        public static bool checkBlocksSameType(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int pos1, ref Vector2Int pos2, ref Vector2Int pos3, ref Vector2Int pos4)
        {
            if (board.ContainsKey(pos1) && board.ContainsKey(pos2) && board.ContainsKey(pos3) && board.ContainsKey(pos4))
                return (board[pos1].Get<BlockType>().value == board[pos2].Get<BlockType>().value
                    && board[pos1].Get<BlockType>().value == board[pos3].Get<BlockType>().value
                    && board[pos1].Get<BlockType>().value == board[pos4].Get<BlockType>().value) ? true : false;

            return false;
        }

        public static List<Vector2Int> getNearbyObstacles(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            var coords = new List<Vector2Int>();

            foreach (var direction in _directions)
            {
                var coordToCheck = position + direction;
                if (board.ContainsKey(coordToCheck) && board[coordToCheck].Get<BlockType>().value == BlockTypes.Obstacle)
                {
                    coords.Add(coordToCheck);
                }
            }

            return coords;
        }

        public static bool hasNearbySameType(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position, ref BlockTypes blockType, bool checkBetween = false)
        {
            foreach (var direction in _directions)
            {
                // три в ряд
                var firstNearbyInLine = position + direction;
                var firstNearbyInLineType = board.ContainsKey(firstNearbyInLine) ? board[firstNearbyInLine].Get<BlockType>().value : BlockTypes.Default;

                var secondNearbyInLine = position + direction + direction;
                var secondNearbyInLineType = board.ContainsKey(secondNearbyInLine) ? board[secondNearbyInLine].Get<BlockType>().value : BlockTypes.Default;

                if (firstNearbyInLineType == blockType && secondNearbyInLineType == blockType)
                    return true;

                // квадрат
                var rightDirection = direction == Vector2Int.up ? Vector2Int.right
                    : direction == Vector2Int.right ? Vector2Int.down
                    : direction == Vector2Int.down ? Vector2Int.left
                    : direction == Vector2Int.left ? Vector2Int.up
                    : Vector2Int.zero;

                var nearbyRight = position + rightDirection;
                var nearbyRightType = board.ContainsKey(nearbyRight) ? board[nearbyRight].Get<BlockType>().value : BlockTypes.Default;

                var diagonally = position + direction + rightDirection;
                var diagonallyType = board.ContainsKey(diagonally) ? board[diagonally].Get<BlockType>().value : BlockTypes.Default;

                if (firstNearbyInLineType == blockType && nearbyRightType == blockType && diagonallyType == blockType)
                    return true;

                //добавить проверку спавн между 2мя одного типа только для спавн системы
                if (checkBetween)
                {
                    var prevNearbyInLine = position - direction;
                    var prevNearbyInLineType = board.ContainsKey(prevNearbyInLine) ? board[prevNearbyInLine].Get<BlockType>().value : BlockTypes.Default;

                    if (prevNearbyInLineType == blockType && firstNearbyInLineType == blockType)
                        return true;
                }
            }

            return false;
        }

        public static bool isObstacle(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            return board.ContainsKey(position) && board[position].Get<BlockType>().value == BlockTypes.Obstacle;
        }

        public static List<Vector2Int> getCoordToDestroyIfBooster(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            var coords = new List<Vector2Int>();
            var blockType = board[position].Get<BlockType>().value;

            if (blockType == BlockTypes.DestroyLineHorizontal)
            {
                var pos = position + Vector2Int.left;

                while (board.TryGetValue(pos, out var entity))
                {
                    coords.Add(pos);
                    pos += Vector2Int.left;
                }

                pos = position + Vector2Int.right;

                while (board.TryGetValue(pos, out var entity))
                {
                    coords.Add(pos);
                    pos += Vector2Int.right;
                }
            }
            /*else if (blockType == BlockTypes.DestroyLineVertical)
            {
                var pos = position + Vector2Int.up;

                while (board.TryGetValue(pos, out var entity))
                {
                    coords.Add(pos);
                    pos += Vector2Int.up;
                }

                pos = position + Vector2Int.down;

                while (board.TryGetValue(pos, out var entity))
                {
                    coords.Add(pos);
                    pos += Vector2Int.down;
                }
            }*/
            else if (blockType == BlockTypes.DestroyCross)
            {
                foreach (var direction in _directions)
                {
                    var pos = position + direction;

                    while (board.TryGetValue(pos, out var entity))
                    {
                        coords.Add(pos);
                        pos += direction;
                    }
                }
            }
            else if (blockType == BlockTypes.BombSmall)
            {
                foreach (var direction in _directions)
                {
                    var rightDirection = direction == Vector2Int.up ? Vector2Int.right
                        : direction == Vector2Int.right ? Vector2Int.down
                        : direction == Vector2Int.down ? Vector2Int.left
                        : direction == Vector2Int.left ? Vector2Int.up
                        : Vector2Int.zero;

                    var coordToCheck = position + direction;
                    if (board.ContainsKey(coordToCheck))
                    {
                        coords.Add(coordToCheck);
                    }

                    coordToCheck = position + direction + rightDirection;
                    if (board.ContainsKey(coordToCheck))
                    {
                        coords.Add(coordToCheck);
                    }

                }
            }
            else if (blockType == BlockTypes.BombBig)
            {
                foreach (var direction in _directions)
                {
                    var rightDirection = direction == Vector2Int.up ? Vector2Int.right
                        : direction == Vector2Int.right ? Vector2Int.down
                        : direction == Vector2Int.down ? Vector2Int.left
                        : direction == Vector2Int.left ? Vector2Int.up
                        : Vector2Int.zero;

                    var pos = position + direction;

                    var c = 2;
                    while (board.TryGetValue(pos, out var entity))
                    {
                        if (c == 0) break;

                        if (board.ContainsKey(pos))
                        {
                            coords.Add(pos);
                        }

                        if (board.ContainsKey(pos + rightDirection))
                        {
                            coords.Add(pos + rightDirection);
                        }

                        if (board.ContainsKey(pos + rightDirection + rightDirection))
                        {
                            coords.Add(pos + rightDirection + rightDirection);
                        }

                        c--;
                        pos += direction;
                    }
                }
            }

            if (coords.Count > 0) coords.Add(position);

            return coords;
        }

        public static void getBoosterMergeType(this Dictionary<Vector2Int, EcsEntity> board, ref BlockTypes t1, ref BlockTypes t2, out BlockTypes mergeType)
        {
            var key = t1.GetHashCode() + "+" + t2.GetHashCode();
            mergeType = _boosterMergeConfig.ContainsKey(key) ? _boosterMergeConfig[key] : BlockTypes.Default;
        }
    }
}