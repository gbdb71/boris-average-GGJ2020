using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToolBox : MonoBehaviour
{

    [Header("Meshes")]
    public List<int> weights = new List<int>();

    [Header("Logic")]
    private MeshFilter mFilter;
    private List<int> accumulatedWeights = new List<int>();
    private int totalWeight = 0;
    public enum toolType { Empty, Tape, Gum, Sock }
    public toolType handType = toolType.Tape;

    void Awake()
    {
        mFilter = GetComponent<MeshFilter>();
        for (var i = 0; i < weights.Count; i++)
        {
            accumulatedWeights.Add(totalWeight);
            totalWeight += weights[i];
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

    public int SetNextTool() {
       return GetNextIndex();
    }

 /*   public Mesh SetNextTool(int index) {
       return meshes[index];
   }*/
}
