using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchNavNode : NavNode
{
    public NavNode parent { get; set; } = null;
    public bool visited { get; set; } = false;
}
