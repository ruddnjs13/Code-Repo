using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/Sound/SfxList")]
public class SfxListData : ScriptableObject
{
    public List<SfxSO> sfxList;
}