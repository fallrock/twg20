using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public ulong collected = 0;
    public ulong toCollect = 1;
    public bool won = false;

    public void Collect()
    {
        if (won) return;

        collected++;
        CheckWin();
    }

    private void CheckWin()
    {
        if (collected >= toCollect)
        {
            won = true;

            ///TODO: win the fucking game
            Debug.Log("won");
        }
    }
}
