using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stage : MonoBehaviour
{
    public List<Tilemap> groundTilemaps;
    public List<Tilemap> wallTilemaps;
    public List<Tilemap> goalTilemaps;
    public List<int> goals;
    public List<GameObject> boxes;
    public Door enterDoor;
    public Door exitDoor;

    public void Initialize(int loopIndex)
    {
        HideEverything();

        groundTilemaps[loopIndex].gameObject.SetActive(true);
        wallTilemaps[loopIndex].gameObject.SetActive(true);
        goalTilemaps[loopIndex].gameObject.SetActive(true);
        for (int i = 0; i < loopIndex + 1; i++)
        {
            var box = boxes[i];
            box.gameObject.SetActive(true);
        }

        enterDoor.Initialize();
        exitDoor.Initialize();
    }

    void HideEverything()
    {
        groundTilemaps.ForEach(t => t.gameObject.SetActive(false));
        wallTilemaps.ForEach(t => t.gameObject.SetActive(false));
        goalTilemaps.ForEach(t => t.gameObject.SetActive(false));
        boxes.ForEach(t => t.gameObject.SetActive(false));
    }
}