using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;

public class LoginControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Serializable]
    public class Info
    {

    }

    public void LoginFacebook()
    {
        FB.Init(delegate() {
            FB.LogInWithReadPermissions(
                new List<string>() { "public_profile", "email", "user_friends" }, 
                delegate(ILoginResult result) {
                    Debug.Log(result.RawResult);
                    string user_id = result.ResultDictionary["user_id"].ToString();
                    string photo_url = "http://graph.facebook.com/" + user_id + "/picture?type=square";
                    Debug.Log(user_id);
                    Debug.Log(photo_url);

                    FB.API("/me", HttpMethod.GET, delegate (IGraphResult meResult)
                    {
                        Debug.Log(meResult.RawResult);
                        string facebook_Name = meResult.ResultDictionary["name"].ToString();
                        Debug.Log("Facebook name : " + facebook_Name);
                    });

                    FB.API("/me/friends", HttpMethod.GET, delegate (IGraphResult friendResult)
                    {
                        Debug.Log(friendResult.RawResult);
                        FriendResult res = JsonUtility.FromJson<FriendResult>(friendResult.RawResult);
                        Debug.Log(res.summary.total_count);
                        
                    });
                });
        });
    }
}

[Serializable]
public class Friend
{
    public string name;
    public string user_id;
}

[Serializable]
public class FriendSummary
{
    public int total_count;    
}

[Serializable]
public class FriendResult
{
    public Friend[] data;
    public FriendSummary summary;
}
