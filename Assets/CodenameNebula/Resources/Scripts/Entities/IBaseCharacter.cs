using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBaseCharacter
{
    //[Header("Stats")]
    CharacterStats CharStats { get; set; }

}

[System.Serializable]
public class CharacterStats
{
    public float health;
    public float shield;
    public float speed;
    //test values
    //public float test1 = 4;
    //public float test2 = 90.8f;

    public CharacterStats() { health = 200; shield = 100; }

    public CharacterStats(float hp, float shield)
    {
        this.health = hp;
        this.shield = shield;
    }
}
