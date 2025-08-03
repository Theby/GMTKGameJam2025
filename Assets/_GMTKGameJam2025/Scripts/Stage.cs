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

    List<List<Vector3>> _boxPositions = new List<List<Vector3>>();

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

    public void SaveOriginalBoxPositions()
    {
        _boxPositions = new List<List<Vector3>>();

        for (var i = 0; i < boxes.Count; i++)
        {
            _boxPositions.Add(new List<Vector3>());
            var boxParent = boxes[i];
            foreach (Transform box in boxParent.transform)
            {
                _boxPositions[i].Add(box.position);
            }
        }
    }

    public void ResetAllBoxes()
    {
        for (var i = 0; i < boxes.Count; i++)
        {
            var boxParent = boxes[i];
            var j = 0;
            foreach (Transform box in boxParent.transform)
            {
                box.position = _boxPositions[i][j];
                j++;
            }
        }
    }
}