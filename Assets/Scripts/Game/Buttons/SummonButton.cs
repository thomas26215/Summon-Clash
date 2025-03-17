using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SummonButton : MonoBehaviour
{
    [Header("Configuration")]
    public int initialSummonCost = 30;
    public int priceIncreaseAmount = 30;
    public int startingResources = 5000;
    public int maxSpawnAttempts = 6;

    [Header("Références UI")]
    public Text priceText;
    public Button button;

    [Header("Summon Configuration")]
    public Tilemap tilemap;

    [Header("Deck Configuration")]
    public DeckData deckData;

    private int currentSummonCost;
    private int currentResources;

    void Start() => InitializeButton();

    void InitializeButton()
    {
        currentResources = startingResources;
        currentSummonCost = initialSummonCost;

        if (!ValidateReferences())
        {
            enabled = false;
            return;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(TrySummon);
        UpdateUI();
    }

    bool ValidateReferences()
    {
        if (priceText == null) Debug.LogError("PriceText non assigné !");
        if (button == null) Debug.LogError("Button non assigné !");
        if (tilemap == null) Debug.LogError("Tilemap non assigné !");
        if (deckData == null) Debug.LogError("DeckData non assigné !");

        return priceText && button && tilemap && deckData;
    }

    void TrySummon()
    {
        if (HasEnoughResources(currentSummonCost))
        {
            PerformSummon();
        }
        else
        {
            Debug.Log("Ressources insuffisantes !");
        }
    }

    void PerformSummon()
    {
        DeductResources(currentSummonCost);
        if (!TrySummonUnit())
        {
            currentResources += currentSummonCost; // Remboursement
        }
        IncreasePrice();
        UpdateUI();
    }

    bool TrySummonUnit()
    {
        var character = GetRandomCharacter();
        var position = FindFreeTile();

        if (position == Vector3Int.one * -1 || character?.prefab == null)
        {
            Debug.Log("Échec de l'invocation");
            return false;
        }

        InstantiateCharacter(character, position);
        return true;
    }

    void InstantiateCharacter(CharacterData character, Vector3Int tilePosition)
    {
        Vector3 worldPos = tilemap.GetCellCenterWorld(tilePosition);
        GameObject unit = Instantiate(character.prefab, worldPos, Quaternion.identity);
        
        if (!unit.GetComponent<Collider2D>())
        {
            unit.AddComponent<BoxCollider2D>();
        }
        
        Debug.Log($"Invoqué : {character.characterName} ({character.rarity})");
    }

    CharacterData GetRandomCharacter()
    {
        float rand = Random.value;
        List<CharacterData> selectedList = null;

        if (rand <= DeckData.COMMON_PROBABILITY) selectedList = deckData.commonCharacters;
        else if (rand <= DeckData.COMMON_PROBABILITY + DeckData.UNCOMMON_PROBABILITY) selectedList = deckData.uncommonCharacters;
        else if (rand <= DeckData.COMMON_PROBABILITY + DeckData.UNCOMMON_PROBABILITY + DeckData.RARE_PROBABILITY) selectedList = deckData.rareCharacters;
        else if (rand <= DeckData.COMMON_PROBABILITY + DeckData.UNCOMMON_PROBABILITY + DeckData.RARE_PROBABILITY + DeckData.EPIC_PROBABILITY) selectedList = deckData.epicCharacters;
        else if (rand <= DeckData.COMMON_PROBABILITY + DeckData.UNCOMMON_PROBABILITY + DeckData.RARE_PROBABILITY + DeckData.EPIC_PROBABILITY + DeckData.LEGENDARY_PROBABILITY) selectedList = deckData.legendaryCharacters;
        else selectedList = deckData.mythicCharacters;

        return SelectFromList(selectedList);
    }

    CharacterData SelectFromList(List<CharacterData> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("Liste vide - Retour aux communs");
            return deckData.commonCharacters.Count > 0 ? 
                deckData.commonCharacters[Random.Range(0, deckData.commonCharacters.Count)] : 
                null;
        }
        return list[Random.Range(0, list.Count)];
    }

    Vector3Int FindFreeTile(int y = 2)
    {
        for (int x = 0; x < maxSpawnAttempts; x++)
        {
            Vector3Int pos = new Vector3Int(x - 4, y, 0);
            if (IsCellFree(tilemap.GetCellCenterWorld(pos))) return pos;
        }
        return y > 0 ? FindFreeTile(y - 1) : Vector3Int.one * -1;
    }

    bool IsCellFree(Vector3 position) => 
        Physics2D.OverlapBoxAll(position, new Vector2(0.9f, 0.9f), 0).Length == 0;

    // Méthodes utilitaires restantes
    void IncreasePrice() => currentSummonCost += priceIncreaseAmount;
    void UpdateUI()
    {
        priceText.text = currentSummonCost.ToString();
        button.interactable = HasEnoughResources(currentSummonCost);
    }

    bool HasEnoughResources(int cost) => currentResources >= cost;
    void DeductResources(int cost) => currentResources = Mathf.Max(0, currentResources - cost);
}

