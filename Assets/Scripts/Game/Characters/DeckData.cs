using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDeckData", menuName = "Scriptable Objects/DeckData")]
public class DeckData : ScriptableObject
{
    [Header("Personnages")]
    public List<CharacterData> commonCharacters = new List<CharacterData>();
    public List<CharacterData> uncommonCharacters = new List<CharacterData>();
    public List<CharacterData> rareCharacters = new List<CharacterData>();
    public List<CharacterData> epicCharacters = new List<CharacterData>();
    public List<CharacterData> legendaryCharacters = new List<CharacterData>();
    public List<CharacterData> mythicCharacters = new List<CharacterData>();

    [Header("Probabilités")]
    public const float COMMON_PROBABILITY = 0.47f;
    public const float UNCOMMON_PROBABILITY = 0.32f;
    public const float RARE_PROBABILITY = 0.15f;
    public const float EPIC_PROBABILITY = 0.05f;
    public const float LEGENDARY_PROBABILITY = 0.008f;
    public const float MYTHIC_PROBABILITY = 0.002f;

    private void OnValidate() => VerifyProbabilities();

    void VerifyProbabilities()
    {
        float total = COMMON_PROBABILITY + UNCOMMON_PROBABILITY + RARE_PROBABILITY + 
                     EPIC_PROBABILITY + LEGENDARY_PROBABILITY + MYTHIC_PROBABILITY;
        
        if (Mathf.Abs(total - 1f) > 0.001f)
        {
            Debug.LogWarning($"Probabilités totales = {total:P1} - Doit être 100%");
        }
    }
}

