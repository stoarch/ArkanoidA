using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    BlockData blockData;
    [SerializeField]
    LevelManager manager;

    public BlockData Data => blockData;
}
