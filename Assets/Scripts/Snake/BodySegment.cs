using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySegment : Segment
{
    public List<Vector3> prePositions;
    public List<Quaternion> preRotations;

    [SerializeField]
    protected Transform preSegment;

    void FixedUpdate()
    {
        if (SnakeHead.moving)
        {
            GetPosAndRot();

            if (movable)
            {
                TranslateAndRotate();
                AdjustLists();
            }
        }
    }

    protected void GetPosAndRot()
    {
        prePositions.Add(preSegment.position);
        preRotations.Add(preSegment.rotation);
    }

    protected void TranslateAndRotate()
    {
        transform.position = prePositions[0];
        transform.rotation = preRotations[0];
    }

    protected void AdjustLists()
    {
        prePositions.RemoveAt(0);
        preRotations.RemoveAt(0);
    }

    public void NewSegmentData(List<Vector3> poses, List<Quaternion> rots, Transform preS)
    {
        movable = true;
        ClearLists();

        foreach (Vector3 pos in poses)
        {
            prePositions.Add(pos);
        }

        foreach (Quaternion rot in rots)
        {
            preRotations.Add(rot);
        }

        preSegment = preS;
    }
    
    protected void ClearLists()
    {
        prePositions.Clear();
        preRotations.Clear();
    }
}
