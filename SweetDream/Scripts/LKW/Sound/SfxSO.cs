using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound/SfxSO")]
public class SfxSO : ScriptableObject
{
    public AudioClip clip;
    public SFXEnum sfxType;
}
