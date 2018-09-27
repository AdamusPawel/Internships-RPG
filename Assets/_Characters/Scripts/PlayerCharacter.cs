using System;
using RPG.Characters;
using UnityEditor;
using UnityEngine;

public class PlayerCharacter : Character
{
    #region Player statictics

    [Header("Player statistics")]
    [SerializeField] int experiencePoints = 0;
    [SerializeField] int level = 1;
    [Space(10)]
    [SerializeField] int vitality = 10;
    [SerializeField] int strength = 10;
    [SerializeField] int dexterity = 10;

    #endregion

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Draw movement line
            Handles.color = Color.black;
            Handles.DrawLine(transform.position, navMeshAgent.destination);
            Handles.DrawSolidDisc(navMeshAgent.destination, new Vector3(0, 1, 0), 0.1f);
            Handles.DrawWireDisc(navMeshAgent.destination, new Vector3(0, 1, 0), navMeshAgent.stoppingDistance);
    }
}
