using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class DeckData : ScriptableObject
{
    public List<CharacterData> commonCharacters = new List<CharacterData>();
    public List<CharacterData> uncommonCharacters = new List<CharacterData>();
    public List<CharacterData> rareCharacters = new List<CharacterData>();
    public List<CharacterData> epicCharacters = new List<CharacterData>();
    public List<CharacterData> legendaryCharacters = new List<CharacterData>();
    public List<CharacterData> mythicCharacters = new List<CharacterData>();
}
