using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(menuName = "SO/Character")]
public class CharacterDataSO : ScriptableObject
{
    public string CharNmae;
    public SpriteLibraryAsset library;
    public int damage;
    public int health;
    public string weapon;
    public bool available;
}
