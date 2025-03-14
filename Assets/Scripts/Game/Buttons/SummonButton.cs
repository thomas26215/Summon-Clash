using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

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
    public GameObject characterPrefab;

    private int currentSummonCost;
    private int currentResources;

    void Start()
    {
        InitializeButton();
    }

    void InitializeButton()
    {
        currentResources = startingResources;
        currentSummonCost = initialSummonCost;

        if (priceText == null)
        {
            Debug.LogError("ERREUR : priceText (UnityEngine.UI.Text) n'est pas assigné !");
            enabled = false;
            return;
        }

        if (button == null)
        {
            button = GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError("ERREUR : Button n'est pas assigné !");
                enabled = false;
                return;
            }
        }

        if (tilemap == null || characterPrefab == null)
        {
            Debug.LogError("ERREUR : Tilemap ou CharacterPrefab n'est pas assigné !");
            enabled = false;
            return;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        UpdatePriceText();
        UpdateInteractableState();
    }

    void OnClick()
    {
        TrySummon();
    }

    void TrySummon()
    {
        if (HasEnoughResources(currentSummonCost))
        {
            PerformSummon();
        }
        else
        {
            HandleInsufficientResources();
        }
    }

    void PerformSummon()
    {
        DeductResources(currentSummonCost);
        SummonUnit();
        IncreasePrice();
        UpdateUI();
    }

    void HandleInsufficientResources()
    {
        Debug.Log("Pas assez de ressources !");
    }

    void IncreasePrice()
    {
        currentSummonCost += priceIncreaseAmount;
    }

    void UpdateUI()
    {
        UpdatePriceText();
        UpdateInteractableState();
    }

    void UpdatePriceText()
    {
        priceText.text = currentSummonCost.ToString();
    }

    void UpdateInteractableState()
    {
        button.interactable = HasEnoughResources(currentSummonCost);
    }

    void SummonUnit()
    {
        Vector3Int tilePosition = FindFreeTile();
        if (tilePosition != Vector3Int.one * -1)
        {
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            GameObject summonedUnit = Instantiate(characterPrefab, worldPosition, Quaternion.identity);
            
            // Assurez-vous que l'unité a un Collider2D
            if (summonedUnit.GetComponent<Collider2D>() == null)
            {
                summonedUnit.AddComponent<BoxCollider2D>();
            }
            
            Debug.Log($"Unité invoquée sur la case {tilePosition}!");
        }
        else
        {
            Debug.Log("Aucune case libre trouvée pour l'invocation!");
            currentResources += currentSummonCost; // Remboursement du coût
        }
    }

    Vector3Int FindFreeTile(int y = 2)
    {
        for (int x = 0; x < maxSpawnAttempts; x++)
        {
            Vector3Int tilePosition = new Vector3Int(x-4, y, 0);
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            Debug.Log($"Tentative d'invocation à la position : {worldPosition}");
            if (IsCellFree(worldPosition))
            {
                return tilePosition;
            }
        }
        if(y > 0) {
            return FindFreeTile(y-1);
        }
        return Vector3Int.one * -1;
    }

    bool IsCellFree(Vector3 position)
    {
        Vector2 boxSize = new Vector2(0.9f, 0.9f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(position, boxSize, 0);
        return hits.Length == 0;
    }

    bool HasEnoughResources(int cost)
    {
        return currentResources >= cost;
    }

    void DeductResources(int cost)
    {
        currentResources -= cost;
        Debug.Log("Ressources déduites. Ressources restantes: " + currentResources);
    }
}

