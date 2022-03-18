using System;

[Serializable]
public class JsonGhostSave
{
    public JsonGhostTransform[] ghostTransforms = new JsonGhostTransform[0];
}

[Serializable]
public class JsonGhostTransform
{

    public JsonPosition position;
    public JsonRotation rotation;

}

[Serializable]
public class JsonPosition
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class JsonRotation
{
    public float x;
    public float y;
    public float z;
    public float w;
}
