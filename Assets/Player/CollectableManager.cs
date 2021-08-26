using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public ulong collected = 0;
    public ulong toCollect = 1;

    public WinLogic winLogic;

    public void Collect()
    {
        collected++;
        CheckWin();
    }

    private void CheckWin()
    {
        if (collected >= toCollect)
        {
            winLogic.Win();
        }
    }
}
