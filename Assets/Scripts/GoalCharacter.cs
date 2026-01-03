using System.Collections.Generic;
using UnityEngine;

public class GoalCharacter : WinCondition
{
    public LayerMask goalLayer;
    public List<int> characterIndexs;

    public override bool IsWinConditionMet()
    {
        int neededCount = characterIndexs.Count;
        int currentCount = 0;
        var hits = Physics2D.OverlapCircleAll(transform.position, 0.1f, goalLayer);
        foreach (var hit in hits)
        {
            var goal = hit.GetComponent<CharacterCommandController>();
            if (goal != null && characterIndexs.Contains(goal.spawnIndex))
            {
                currentCount++;
            }
        }
        
        return currentCount == neededCount;
    }
}