using System;
using System.Collections.Generic;

namespace Match3
{
    //[Serializable]
    //public class LevelData
    //{
    //    public int Stars = 0;
    //}

    [Serializable]
    public class SaveData
    {
        public int Level = 0;
        public float TimeElapsed = 0;
        public int Stars = 0;
       // public List<LevelData> levels;
    }
}