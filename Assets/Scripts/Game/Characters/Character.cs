using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData;
    public int currentHealth;

    void Awake()
    {
        Debug.Log("Awake called on " + gameObject.name);
        InitializeCharacter();
    }

    void Start()
    {
        Debug.Log("Start called on " + gameObject.name);
        // Vector3 spawnPosition = new Vector3(0, 0, 0);
        // SpawnCharacter(spawnPosition);
    }

    void InitializeCharacter()
    {
        if (characterData != null)
        {
            currentHealth = characterData.maxHealth;
            Debug.Log("Character initialized with " + currentHealth + " health");
        }
        else
        {
            Debug.LogError("CharacterData is not assigned in the Inspector for " + gameObject.name + "!");
        }
    }

    public virtual void Attack()
    {
        Debug.Log("Attaque de base!");
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Je suis mort!");
        Destroy(gameObject);
    }

    public static Character SpawnCharacter(CharacterData data, Vector3 position)
    {
        Debug.Log("Attempting to spawn character");
        if (data == null)
        {
            Debug.LogError("CharacterData is null!");
            return null;
        }
        if (data.prefab == null)
        {
            Debug.LogError("Prefab in CharacterData is null!");
            return null;
        }

        GameObject characterObject = Instantiate(data.prefab, position, Quaternion.identity);
        Debug.Log("Character object instantiated: " + characterObject.name);

        Character spawnedCharacter = characterObject.GetComponent<Character>();

        if (spawnedCharacter != null)
        {
            spawnedCharacter.characterData = data;
            spawnedCharacter.InitializeCharacter();
            Debug.Log($"Personnage {data.characterName} invoqué avec {spawnedCharacter.currentHealth} points de vie.");
        }
        else
        {
            Debug.LogError("Le prefab n'a pas de composant Character!");
            Destroy(characterObject);
        }

        return spawnedCharacter;
    }
}

