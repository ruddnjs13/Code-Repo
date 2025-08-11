using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound/BgmSO")]
public class BgmSO : ScriptableObject
{
    public AudioClip clip;
    public BGMEnum bgmType;
}
