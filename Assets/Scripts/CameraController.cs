/*
CinemachineBrain:
Description: CinemachineBrain is a component in Unity's Cinemachine framework, designed for managing and controlling virtual cameras in a scene.
Purpose: CinemachineBrain orchestrates the behavior of multiple virtual cameras, deciding which one should be active at any given time. 
It handles camera blending, priorities, and transitions to create smooth and cinematic camera movements. This component is
particularly useful for complex camera setups in games and cinematics, providing a high level of control over camera behavior and transitions.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CinemachineBrain theCMBrain;

    private void Awake()
    {
        instance = this;
    }   
}