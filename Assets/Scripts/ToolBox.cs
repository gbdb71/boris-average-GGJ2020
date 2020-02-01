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
            accumulatedWeights.Add(totalWeight);
            totalWeight += weights[i];
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
        int seed = Random.Range(0, totalWeight);
        for (var i = 0; i < accumulatedWeights.Count; i++)
        {
			if (seed == accumulatedWeights[i])
            {
                return i;
			}
            if (seed < accumulatedWeights[i]) {
                int index = (i - 1 >= 0)?  i - 1 : 0;
                return index;
			}
        }
        return accumulatedWeights.Count - 1;
    }

    public void SetNextTool() {
        mFilter.mesh = meshes[GetNextIndex()];
    }

    public void SetNextTool(int index) {
        mFilter.mesh = meshes[index];
    }
}
