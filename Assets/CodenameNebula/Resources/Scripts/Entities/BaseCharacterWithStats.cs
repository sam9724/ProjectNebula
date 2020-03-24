using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacterWithStats : MonoBehaviour
{
    [Header("Stats")]
    public CharacterStats statValues;

}

[System.Serializable]
public class CharacterStats
{
    public float health;
    public float stamina;
    public float eezo;
    public float speed;
    //test values
    public float test1 = 4;
    public float test2 = 90.8f;

    public CharacterStats() { health = 200; stamina = 100; eezo = 50; }

    public CharacterStats(float hp, float stamina, float eezo)
    {
        this.health = hp;
        this.stamina = stamina;
        this.eezo = eezo;
    }
}
