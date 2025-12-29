using System.Collections.Generic;
using UnityEngine;

namespace _00Work.LKW.Code.ETC
{
    [CreateAssetMenu(fileName = "StageList", menuName = "SO/StageList", order = 0)]
    public class StageListSO : ScriptableObject
    {
        public List<StageSO> stages = new List<StageSO>();

        public StageSO this[int index] => stages[index];
        
    }
}