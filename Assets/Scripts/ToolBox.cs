using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToolBox : MonoBehaviour
{

    [Header("Meshes")]
    public List<Mesh> meshes = new List<Mesh>();
    public List<int> weights = new List<int>();

    [Header("Logic")]
    private MeshFilter mFilter;
    private List<int> accumulatedWeights = new List<int>();
    private int totalWeight = 0;

    void Awake()
    {
        mFilter = GetComponent<MeshFilter>();
        for (var i = 0; i < weights.Count; i++)
        {
            totalWeight += weights[i];
            accumulatedWeights.Add(totalWeight);
        }
        Debug.Log("Test = " + string.Join(", ",
             accumulatedWeights
             .ConvertAll(i => i.ToString())
             .ToArray()));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetNextTool();
		}
    }

    private int GetNextIndex() {
        int seed = Random.Range(0, totalWeight - 1);
        Debug.Log(seed);
        int index = accumulatedWeights.BinarySearch(seed);
        Debug.Log((index >= 0) ? index : ~index - 1);
        return (index >= 0) ? index : ~index - 1;
    }

    public void SetNextTool() {
        mFilter.mesh = meshes[GetNextIndex()];
    }
}
