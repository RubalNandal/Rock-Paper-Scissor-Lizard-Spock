using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Create Attack/Create New Attack")]
public class BaseAttack : ScriptableObject
{
   public int id;
   public Sprite attackImage;

   public AttackType attackType;

   public List<AttackType> weakAgainst;
   
   public List<AttackType> strongAgainst;

   public BaseAttack(){
       attackType = AttackType.Empty;
   }
}

public enum AttackType{
    Empty,
    Rock,
    Paper,
    Scissors,
    Lizard,
    Spock

}