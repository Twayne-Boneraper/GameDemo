using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    void pauseGame()
    {
        Time.timeScale = 0;
    }
}
