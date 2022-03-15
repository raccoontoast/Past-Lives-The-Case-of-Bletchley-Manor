using System.Collections.Generic;
using UnityEngine;

public class RecordInfo
{
    public enum Interaction
    {
        Action,
        Fire,
        None
    }

    public List<Vector3> PositionList;
    public List<Interaction> ActionList;
    public List<Quaternion> CameraXRotationList;
    public List<Quaternion> CameraYRotationList;

    public void Clear()
    {
        PositionList.Clear();
        ActionList.Clear();
        CameraXRotationList.Clear();
        CameraYRotationList.Clear();
    }

    public RecordInfo()
    {
        PositionList = new List<Vector3>();
        ActionList = new List<Interaction>();
        CameraXRotationList = new List<Quaternion>();
        CameraYRotationList = new List<Quaternion>();
    }

    public void CopyInfoFrom(RecordInfo otherInfo)
    {
        PositionList = new List<Vector3>(otherInfo.PositionList);
        ActionList = new List<Interaction>(otherInfo.ActionList);
        CameraXRotationList = new List<Quaternion>(otherInfo.CameraXRotationList);
        CameraYRotationList = new List<Quaternion>(otherInfo.CameraYRotationList);
    }
}
