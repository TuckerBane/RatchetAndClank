using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawnTreeMiddleNode : BaseSpawnTreeNode
{
    protected BaseSpawnTreeNode[] childNodes;
    protected int[] useCounts;
    protected int curSpawnIndex = 0;


    private void Initialize()
    {
        childNodes = GetComponentsInChildren<BaseSpawnTreeNode>().Where(node => node.transform.parent == transform).ToArray();
        if (!childNodes.Any())
            Debug.LogError("middle node " + this.gameObject.name + " doesn't have any children");
        ReadyTheDeck();
    }

    void Awake()
    {
        if (childNodes == null)
            Initialize();
    }

    void OnEnable()
    {
        if (childNodes == null)
            Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (childNodes == null)
            Initialize();
    }


    protected void ReadyTheDeck()
    {
        curSpawnIndex = 0;
        RandomExtensions.Shuffle(childNodes);
    }

    public override void Place(GameObject newObject)
    {
        curSpawnIndex = ++curSpawnIndex;
        if(curSpawnIndex >= childNodes.Length)
        {
            ReadyTheDeck();
        }

        childNodes[curSpawnIndex].Place(newObject);
    }

}