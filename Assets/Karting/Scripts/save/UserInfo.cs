using System;

[Serializable]
public class UserInfo
{

    public string name;
    public float gameTime;
    
}

[Serializable]
public class RootUserInfo
{
    public UserInfo[] users;
}
