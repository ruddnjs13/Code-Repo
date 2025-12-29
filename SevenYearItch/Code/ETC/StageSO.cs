#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace _00Work.LKW.Code.ETC
{
    [CreateAssetMenu(fileName = "Stage", menuName = "SO/Stage", order = 0)]
    public class StageSO : ScriptableObject
    {
        public string stageName;
        public int necessaryHeart;
        public int goalCount;
    }
}