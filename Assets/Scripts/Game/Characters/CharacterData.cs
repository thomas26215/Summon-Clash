using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public GameObject prefab; //prefab de l'unité
    //public Rarity rarity;

    public int maxHealth;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
}
