using UnityEngine;
using UnityEngine.Tilemaps;

public class SummonManager : MonoBehaviour
{
    [Header("Data")]
    public DeckData deckData;
    public int currentGold;

    [Header("Spawn Points")]
    public Tilemap tileMap;
    public GameObject[] characterPrefabs;

    void Start()
    {
        Vector3Int[] tilePositions = new Vector3Int[] {
            new Vector3Int(0, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(2, 0, 0),
        };

        foreach(var tilePosition in tilePositions)
        {
            Vector3 worldPosition = tileMap.GetCellCenterWorld(tilePosition);
            Instantiate(characterPrefabs[0], worldPosition, Quaternion.identity);
        }
    }
}
