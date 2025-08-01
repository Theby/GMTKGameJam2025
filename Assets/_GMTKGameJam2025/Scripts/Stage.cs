using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stage : MonoBehaviour
{
    public List<Tilemap> groundTilemaps;
    public List<Tilemap> wallTilemaps;
    public List<Tilemap> goalTilemaps;
    public List<GameObject> boxes;
    public List<int> goals;

    public void Initialize(int loopIndex)
    {
        HideEverything();

        groundTilemaps[loopIndex].gameObject.SetActive(true);
        wallTilemaps[loopIndex].gameObject.SetActive(true);
        goalTilemaps[loopIndex].gameObject.SetActive(true);
        for (int i = 0; i < loopIndex + 1; i++)
        {
            boxes[i].gameObject.SetActive(true);
        }
    }

    void HideEverything()
    {
        groundTilemaps.ForEach(t => t.gameObject.SetActive(false));
        wallTilemaps.ForEach(t => t.gameObject.SetActive(false));
        goalTilemaps.ForEach(t => t.gameObject.SetActive(false));
        boxes.ForEach(t => t.gameObject.SetActive(false));
    }
}