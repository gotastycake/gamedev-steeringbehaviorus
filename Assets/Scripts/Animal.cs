using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal: MonoBehaviour
{
    public float wanderForce, maxSpeed, minDistToRun;
    public enum Behavior {wander, run};
    public int updateDelay;
    public Behavior currentBehaviour;
}
