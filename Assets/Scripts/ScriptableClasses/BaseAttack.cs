using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a base attack in the game.
/// </summary>
[CreateAssetMenu(fileName = "Attack", menuName = "Create Attack/Create New Attack")]
public class BaseAttack : ScriptableObject
{
    /// <summary>
    /// The ID of the attack.
    /// </summary>
   public int id;

    /// <summary>
    /// The image representing the attack.
    /// </summary>
   public Sprite attackImage;

    /// <summary>
    /// The type of the attack.
    /// </summary>
   public AttackType attackType;

    /// <summary>
    /// The list of attack types that this attack is strong against.
    /// </summary>
    public List<AttackAgainst> strongAgainst;

    /// <summary>
    /// The list of attack types that this attack is weak against.
    /// </summary>
    public List<AttackAgainst> weakAgainst;

    //public List<AttackAgainst> strongAgainst;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseAttack"/> class.
    /// </summary>
    public BaseAttack(){
       attackType = AttackType.Empty;
   }
}


/// <summary>
/// Represents the types of attacks in the game.
/// </summary>
public enum AttackType{
    Empty,
    Rock,
    Paper,
    Scissors,
    Lizard,
    Spock

}

[Serializable]
public struct AttackAgainst
{
    public AttackType attackType;
    public string description;
}