using System;

[Serializable]
public class UserInfo
{

    public string name;
    public float gameTime;
    public string ghost;

}

[Serializable]
public class RootUserInfo
{
    public UserInfo[] users;
}
