using System;
using UnityEngine;


namespace Save
{
    [Serializable]
    public class LevelsData
    {
        public bool IsFirstStart;
        public int NumberSaveLevel;

        public int[] ItemID;
        public bool[] ItemClick;

        public LevelsData(LevelManager levelManager)
        {
            
        }
    }
}
