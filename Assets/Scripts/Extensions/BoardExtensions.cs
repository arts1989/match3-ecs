using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public static class BoardExtensions
    {
        static private List<Vector2Int> _directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };
        static private int _chainLenght = 3;

        public static (List<Vector2Int> coords, BlockTypes blockType) getMatchCoords(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position, ref Vector2Int oldPosition)
        {
            var matchCoords = new List<Vector2Int>();
            var coords = new List<Vector2Int>();

            bool hasDestroyLineHorizontal = false;
            bool hasDestroyLineVertical = false;
            bool hasDestroyCross = false;
            bool hasHoming = false;
            bool hasBombSmall = false;

            // �������� ���� 3 � ���
            foreach (var direction in _directions)
            {
                if (direction + position == oldPosition)
                    continue; // ������ ������, �� ��������, ��� ��� ������� ����

                var chainLenght = 1;
                var startPos = position;
                coords.Clear();

                if (direction != (oldPosition - position) && board.ContainsKey(position - direction)) // ������� -1 ��������� ��� �������� ������ "������ ����� ����� ����������� ����"
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

                    // ������ _|_ - ������� �������� ���� �� ���� � ������� ����� ������ ���� ���� �� (��������) ����
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

                                hasBombSmall = true;
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

                                hasBombSmall = true;
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

                                hasBombSmall = true;
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

                                hasBombSmall = true;
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

            if (matchCoords.Count == 4 && hasBombSmall == false) // ������ 4 � ��� ��� 2 ����3 �������
            {
                hasDestroyLineHorizontal = true;
            }

            if (matchCoords.Count >= 5 && hasBombSmall == false)// ������ 5 � ��� ��� 2 ����3 �������
            {
                hasDestroyCross = true;
            }



            // �������� ���������� ������� �� 4
            var swipeDirection = position - oldPosition;
            foreach (var direction in _directions)
            {
                if (direction + position == oldPosition || position - direction == oldPosition)
                    continue; // ������� ����������� ������ � �������

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

                    hasHoming = true;
                }
            }

            if (matchCoords.Count < _chainLenght)
                matchCoords.Clear();

            var boosterTypeToSpawnOnCurrentPosition = hasBombSmall ? BlockTypes.BombSmall
                : hasHoming ? BlockTypes.Homing
                : hasDestroyLineHorizontal ? BlockTypes.DestroyLineHorizontal
                : hasDestroyLineVertical ? BlockTypes.DestroyLineVertical
                : hasDestroyCross ? BlockTypes.DestroyCross
                : BlockTypes.Default;

            return (matchCoords, boosterTypeToSpawnOnCurrentPosition);
        }

        public static bool checkMoveAvaliable(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position, ref Vector2Int swipeDirection)
        {
            if (!board.ContainsKey(position + swipeDirection)) // ����� �� ������� �����
                return false;

            if (board[position].Get<BlockType>().value == BlockTypes.Obstacle ||
                board[position + swipeDirection].Get<BlockType>().value == BlockTypes.Obstacle)
            {
                //Debug.Log("����� �� �������, � ������� �� ���������");
                return false;
            }

            if (board[position].Get<BlockType>().value == BlockTypes.DestroyLineHorizontal || board[position].Get<BlockType>().value == BlockTypes.DestroyLineVertical || board[position].Get<BlockType>().value == BlockTypes.DestroyCross || board[position].Get<BlockType>().value == BlockTypes.Homing || board[position].Get<BlockType>().value == BlockTypes.BombSmall)
            {
                //Debug.Log("BoosterMove");
                return true;
            }

            // �������� ���� 3 � ���
            foreach (var direction in _directions)
            {
                if (direction + swipeDirection == Vector2.zero)
                    continue; // ������� �������� ����������� ������, ��� �� ����� ���� ����������

                var chainLenght = 1;
                var startPos = position + swipeDirection;
                var coordToCheck = 2;

                if (direction != swipeDirection && board.ContainsKey(startPos - direction)) // ������� -1 ��������� ��� �������� ������ "������ ����� ����� ����������� ����"
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

            // �������� ���������� ������� �� 4
            foreach (var direction in _directions)
            {
                if (direction + swipeDirection == Vector2.zero || direction == swipeDirection)
                    continue; // ������� ����������� ������ � �������

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
                // ��� � ���
                var firstNearbyInLine = position + direction;
                var firstNearbyInLineType = board.ContainsKey(firstNearbyInLine) ? board[firstNearbyInLine].Get<BlockType>().value : BlockTypes.Default;

                var secondNearbyInLine = position + direction + direction;
                var secondNearbyInLineType = board.ContainsKey(secondNearbyInLine) ? board[secondNearbyInLine].Get<BlockType>().value : BlockTypes.Default;

                if (firstNearbyInLineType == blockType && secondNearbyInLineType == blockType)
                    return true;

                // �������
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

                //�������� �������� ����� ����� 2�� ������ ���� ������ ��� ����� �������
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

        public static bool isBooster(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            if (board[position].Get<BlockType>().value == BlockTypes.DestroyLineHorizontal || board[position].Get<BlockType>().value == BlockTypes.DestroyLineVertical || board[position].Get<BlockType>().value == BlockTypes.DestroyCross || board[position].Get<BlockType>().value == BlockTypes.Homing || board[position].Get<BlockType>().value == BlockTypes.BombSmall)
            {
                return true;
            }

            return false;
        }

        public static int checkBoosterType(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {

            var type = 0;

            if (board[position].Get<BlockType>().value == BlockTypes.DestroyLineHorizontal)
                 type = 1;

            if (board[position].Get<BlockType>().value == BlockTypes.DestroyLineVertical)
                 type = 2;

            if (board[position].Get<BlockType>().value == BlockTypes.DestroyCross)
                 type = 3;

            if (board[position].Get<BlockType>().value == BlockTypes.BombSmall)
                 type = 4;

            if (board[position].Get<BlockType>().value == BlockTypes.Homing)
                 type = 5;


             return type;
        }


        public static List<Vector2Int> boosterLineX(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            var coords = new List<Vector2Int>();
            var pos1 = position;
            var pos2 = position + Vector2Int.left;

            while (board.TryGetValue(pos1, out var entity))
            {
                coords.Add(pos1);
                pos1 += Vector2Int.right;
            }

            while (board.TryGetValue(pos2, out var entity))
            {
                coords.Add(pos2);
                pos2 += Vector2Int.left;
            }


            return coords;
        }

        public static List<Vector2Int> boosterLineY(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            var coords = new List<Vector2Int>();
            var pos1 = position;
            var pos2 = position + Vector2Int.down;

            while (board.TryGetValue(pos1, out var entity))
            {
                coords.Add(pos1);
                pos1 += Vector2Int.up;
            }

            while (board.TryGetValue(pos2, out var entity))
            {
                coords.Add(pos2);
                pos2 += Vector2Int.down;
            }


            return coords;
        }

        public static List<Vector2Int> boosterCross(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            var coords = new List<Vector2Int>();
            var pos1 = position;
            var pos2 = position + Vector2Int.right;
            var pos3 = position + Vector2Int.down;
            var pos4 = position + Vector2Int.left;


            while (board.TryGetValue(pos1, out var entity))
            {
                coords.Add(pos1);
                pos1 += Vector2Int.up;
            }

            while (board.TryGetValue(pos2, out var entity))
            {
                coords.Add(pos2);
                pos2 += Vector2Int.right;
            }

            while (board.TryGetValue(pos3, out var entity))
            {
                coords.Add(pos3);
                pos3 += Vector2Int.down;
            }

            while (board.TryGetValue(pos4, out var entity))
            {
                coords.Add(pos4);
                pos4 += Vector2Int.left;
            }

            coords.Add(position);

            return coords;
        }

        public static List<Vector2Int> boosterBombS(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            var coords = new List<Vector2Int>();
            // var pos1 = position + Vector2Int.up + Vector2Int.left;
            // var pos2 = position + Vector2Int.down + Vector2Int.right;

            foreach (var direction in _directions)
            {
                var coordToCheck = position + direction;
                if (board.ContainsKey(coordToCheck))
                {
                    coords.Add(coordToCheck);
                }
            }
            coords.Add(position);
            return coords;
        }

        public static List<Vector2Int> boosterHoming(this Dictionary<Vector2Int, EcsEntity> board, ref Vector2Int position)
        {
            var coords = new List<Vector2Int>();

            // TO DO

            coords.Add(position);
            return coords;
        }






    }

}