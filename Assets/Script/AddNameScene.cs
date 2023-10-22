using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/*using Enums;
 using Facebook.Unity;
#if UNITY_IOS
 using Unity.Notifications.iOS;
#else
 using Unity.Notifications.Android;
#endif*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

//using Facebook.Unity;

namespace Scenes.Scripts
{
    public class AddNameScene : MonoBehaviour
    {
        #region Components

        public Button AprooveButton;
        public GameObject MaleAvatar;
        public GameObject FemaleAvatar;
        public InputField UserNameInput;
        public Toggle FemaleToggle;
        public Toggle MaleToggle;

        #endregion Components

        // private Regex regex = new Regex(@"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{3,25}$");
        // private string message = "Wróć do gry i zobacz jak potoczą się losy HalXa! Graj i rozwijaj swoje umiejętności.";
        public string PlayerName;
        public string Gender;
        public bool ClearPrefs;
        public Text Kobieta;
        public Text Meszczyzna;

        #region Init

        void Start()
        {

            //FB.Init(this.OnInitComplete,this.OnHideUnity);



            //manager.ScheduleNotification(notification);

            // if (ClearPrefs)

            PlayerPrefs.DeleteAll(); // TODO remove it.
            ResetTaskPercentages();

            GetData();
            PrepareView();

            StartCoroutine(Delay());
        }

        /*private void OnInitComplete()
        {
            // Debug.Log( "Success - Check log for details");
            // Debug.Log( "Success Response: OnInitComplete Called");
            string logMessage = string.Format(
                "OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'",
                FB.IsLoggedIn,
                FB.IsInitialized);
            Debug.Log(logMessage);
            if (AccessToken.CurrentAccessToken != null)
            {
                Debug.Log(AccessToken.CurrentAccessToken.ToString());
            }
        
            FB.LogAppEvent("Install");
            
            //FB.Loginew List<string>() {}, this.HandleResult);
        
        }*/


        private void OnHideUnity(bool isGameShown)
        {
            Debug.Log("Success - Check log for details");
            Debug.Log("Success Response: OnHideUnity Called {0}\n" + isGameShown);
            Debug.Log("Is game shown: " + isGameShown);
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.7f);
            //RunNotifications();
        }


        /*private void RunNotifications()
        {
            
            
#if UNITY_IOS

var timeTrigger = new iOSNotificationTimeIntervalTrigger()
{
    TimeInterval = new TimeSpan(168, 0, 0),
    Repeats = true
};

var notification = new iOSNotification()
{
      Identifier = "_notification_01",
      Title = "YEP",
      Body = message,
     // Subtitle = "This is a subtitle, something, something important...",
      ShowInForeground = true,
      ForegroundPresentationOption = (PresentationOption.Alert),
      CategoryIdentifier = "category_a",
      ThreadIdentifier = "thread1",
      Trigger = timeTrigger,
};

iOSNotificationCenter.ScheduleNotification(notification);
            


#else
            var c = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };
            
            AndroidNotificationCenter.RegisterNotificationChannel(c);
            
           // var notification = new AndroidNotification();
            AndroidNotification[] notification = new AndroidNotification[25];
           
            
            for (int i = 0; i < notification.Length; i++)
            {
                notification[i].Title = "YEP";
                notification[i].Text = message;
               // notification[i].FireTime = System.DateTime.Now.AddMinutes((i) * 3);
                notification[i].FireTime = System.DateTime.Now.AddDays((i + 1) * 7);
                AndroidNotificationCenter.SendNotification(notification[i], "channel_id");
            }
            
//            notification.Title = "SomeTitle";
//            notification.Text = "SomeText";
//            notification.FireTime = System.DateTime.Now.AddSeconds(2);
//            notification.RepeatInterval = TimeSpan.TicksPerSecond(1);
//
//            AndroidNotificationCenter.SendNotification(notification, "channel_id");
            

#endif
        }*/

        private void ResetTaskPercentages()
        {
            /*ResetTasks("Analiza");
            ResetTasks("Przedsiębiorczość");
            ResetTasks("Komunikacja");
            ResetTasks("Podejmowanie decyzji");*/
            PlayerPrefs.SetInt("ProActivPoints", 0);
            PlayerPrefs.Save();
        }

        private void GetData()
        {
            /*PlayerName = LoadData(Prefs.PlayerName);
            Gender = LoadData(Prefs.Gender);*/
        }

        private void PrepareView()
        {
            ActiveButton(false);

            if (string.IsNullOrEmpty(Gender))
                Gender = "female";

            if (Gender == "female")
                FemaleToggle.isOn = true;
            else
                MaleToggle.isOn = true;

            ToogleGender(Gender);

            UserNameInput.text = PlayerName;

            Validation();
        }

        #endregion Init



        public void UserTextChanged()
        {
            PlayerName = UserNameInput.text;

            Validation();
        }

        void Validation()
        {
            if (PlayerName != "" && Gender != "")
                ActiveButton(true);
            else
                ActiveButton(false);
        }

        void ActiveButton(bool state)
        {
            AprooveButton.interactable = state;
        }

        public void ToogleGender(string sex)
        {
            SetAvatar(sex);
            Gender = sex;

            if (Gender != "male")
            {
                Meszczyzna.fontStyle = FontStyle.Normal;
                Kobieta.fontStyle = FontStyle.Bold;
            }
            else
            {
                Meszczyzna.fontStyle = FontStyle.Bold;
                Kobieta.fontStyle = FontStyle.Normal;
            }
        }

        void SetAvatar(string sex)
        {
            MaleAvatar.SetActive(sex == "male");
            FemaleAvatar.SetActive(sex == "female");
        }

        public void PlayGame()
        {
            PlayerPrefs.SetString("Name", PlayerName);
            PlayerPrefs.SetString("Gender", Gender);
            PlayerPrefs.SetFloat("Progress", 0f);
            /*SaveData(Prefs.PlayerName, PlayerName);
            SaveData(Prefs.Gender, Gender);
            SaveData(Prefs.Progress, "0");*/
            SceneManager.LoadScene("MainMenu");

            PlayerPrefs.SetInt("PlayerSetup", 1);
            PlayerPrefs.Save();
        }
    }
}