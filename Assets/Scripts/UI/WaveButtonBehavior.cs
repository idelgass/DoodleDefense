using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveButtonBehavior : MonoBehaviour
{
    public void WaveButtonCLick()
    {
        LevelController.Instance.StartWave();
    }
}
