using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Data")]
public class TurretData : ScriptableObject
{
    public GameObject turretPrefab;
    public int turretCost;
    public Sprite turretIcon;
}
