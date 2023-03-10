using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PFramework;

namespace GameMain
{
    public class Login : MonoBehaviour
    {
        public string key;

        ///  AppDelegate *appDelegate = (AppDelegate *)([UIApplication sharedApplication].delegate);
        /// [appDelegate showUnityView];
        /// [appDelegate sendMessageWithName:"Login" functionName:"LoginByIOS" message:"key"]
        public void LoginByIOS(string data)
        {
            PFramework.Logger.Log(data);
            key = data;
        }
    }
}