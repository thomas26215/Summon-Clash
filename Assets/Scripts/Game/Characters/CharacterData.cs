using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public GameObject prefab;
    public string rarity; // Variable conservée entre parenthèses

    public int maxHealth;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
}

