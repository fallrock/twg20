using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public ulong collected = 0;
    public ulong toCollect = 1;
    public bool won { get; private set; } = false;
    public GameObject levelCompleteUI;

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

            levelCompleteUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
