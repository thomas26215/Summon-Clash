using UnityEngine;
using System.Collections.Generic;

public class Army : MonoBehaviour
{
    public List<CharacterData> characters = new List<CharacterData>();
    public int maxCharacters = 3;
    
    public bool AddCharacter(CharacterData character)
    {
        if (characters.Count < maxCharacters)
        {
            characters.Add(character);
            return true;
        } 
        else 
        {
            Debug.LogWarning("L'armée est pleine, impossible d'ajouter plus de personnages.");
            return false;
        }
    }

    public bool RemoveCharacter(CharacterData character)
    {
        return characters.Remove(character);
    }
}

