using UnityEngine;
using UnityEngine.UI;

public class SummonButton : MonoBehaviour
{
    [Header("Configuration")]
    public int initialSummonCost = 30;
    public int priceIncreaseAmount = 30; // Changement: Montant fixe d'augmentation
    public int startingResources = 500;

    [Header("Références UI")]
    public Text priceText; // **IMPORTANT : Utilise UnityEngine.UI.Text**
    public Button button;

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

        // ***Vérifications des références (TRÈS IMPORTANTES)***
        if (priceText == null)
        {
            Debug.LogError("ERREUR : priceText (UnityEngine.UI.Text) n'est pas assigné ! Assure-toi qu'il est lié dans l'Inspecteur.");
            enabled = false; // Désactive le script pour éviter les erreurs
            return;
        }

        if (button == null)
        {
            button = GetComponent<Button>(); // Récupère le Button sur le même GameObject
            if (button == null)
            {
                Debug.LogError("ERREUR : Button n'est pas assigné ! Assure-toi qu'il est sur le même GameObject que ce script, ou assigne-le dans l'Inspecteur.");
                enabled = false;
                return;
            }
        }

        // Ajout du Listener pour le clic du bouton (UNE SEULE FOIS)
        button.onClick.RemoveAllListeners(); // Retire les listeners existants (pour éviter les doublons)
        button.onClick.AddListener(OnClick);   // Ajoute la fonction OnClick à l'événement du bouton

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
        // TODO: Ajouter un feedback visuel (par exemple, un message à l'écran)
    }

    void IncreasePrice()
    {
        currentSummonCost += priceIncreaseAmount; // Changement: Additionne un montant fixe
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
        // TODO: Remplacer avec la logique réelle de votre jeu pour invoquer l'unité.
        Debug.Log("Unité invoquée !");
    }

    bool HasEnoughResources(int cost)
    {
        return currentResources >= cost;
    }

    void DeductResources(int cost)
    {
        currentResources -= cost;
        Debug.Log("Ressources déduites. Ressources restantes: " + currentResources);
        // TODO: Mettre à jour l'UI pour afficher les ressources restantes.
    }
}

