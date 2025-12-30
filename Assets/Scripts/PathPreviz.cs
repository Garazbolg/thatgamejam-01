using System;
using System.Collections.Generic;
using UnityEngine;

public class PathPreviz : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public PrevizWaitView waitViewPrefab;
    private List<GameObject> waitViews = new List<GameObject>();

    public Color waitColor;

    public void UpdatePreviz(CharacterCommandController characterController)
    {
        ClearPreviz();
        var positions = new List<Vector3>();
        Vector3 currentPosition = characterController.transform.position;
        positions.Add(currentPosition);
        int waitCount = 0;
        for (int i = 0; i < characterController.commands.Count; i++)
        {
            var command = characterController.commands[i];
            if(command != Command.Wait && waitCount > 0)
            {
                var waitView = Instantiate(waitViewPrefab, currentPosition, Quaternion.identity, transform);
                waitView.waitText.text = waitCount.ToString();
                waitView.SetColor(waitColor);
                waitViews.Add(waitView.gameObject);
                waitCount = 0;
            }
            switch (command)
            {
                case Command.MoveUp:
                    currentPosition += Vector3.up;
                    positions.Add(currentPosition);
                    break;
                case Command.MoveDown:
                    currentPosition += Vector3.down;
                    positions.Add(currentPosition);
                    break;
                case Command.MoveLeft:
                    currentPosition += Vector3.left;
                    positions.Add(currentPosition);
                    break;
                case Command.MoveRight:
                    currentPosition += Vector3.right;
                    positions.Add(currentPosition);
                    break;
                case Command.Wait:
                    waitCount++;
                    break;
            }
        }
        if(waitCount > 0)
        {
            var waitView = Instantiate(waitViewPrefab, currentPosition, Quaternion.identity, transform);
            waitView.waitText.text = waitCount.ToString();
            waitView.SetColor(waitColor);
            waitViews.Add(waitView.gameObject);
            waitCount = 0;
        }
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
    
    public void ClearPreviz()
    {
        lineRenderer.positionCount = 0;
        foreach (var waitView in waitViews)
        {
            Destroy(waitView);
        }
        waitViews.Clear();
    }
}