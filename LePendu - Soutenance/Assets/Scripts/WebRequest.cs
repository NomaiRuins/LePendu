using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

public class WebRequest : MonoBehaviour
{
    // NOTIFIE LE GM QUE LE MOT EST PRET
    public static event Action<string> OnWordReady;

    // JSON
    [System.Serializable]
    public class MotData
    {
        public string name;
    }

    [System.Serializable]
    public class MotDataList
    {
        public List<MotData> items;
    }

    [SerializeField]
    string _uri = "https://trouve-mot.fr/api/random";

    public void GetNewWord()
    {
        StartCoroutine(RequestWordCoroutine());
    }

    private IEnumerator RequestWordCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
        
            {
                string jsonResponse = webRequest.downloadHandler.text;
                string wrappedJson = "{\"items\":" + jsonResponse + "}";
                MotDataList wordList = JsonUtility.FromJson<MotDataList>(wrappedJson);

                if (wordList != null && wordList.items != null && wordList.items.Count > 0)
                {
                    string motChoisi = wordList.items[0].name;
                    Debug.Log("Mot de l'API reçu : " + motChoisi);
                    OnWordReady?.Invoke(motChoisi);
                }
                else
                {
                    OnWordReady?.Invoke(null);
                }
            }
            else
            {
                Debug.LogError("Une erreur réseau est survenue: " + webRequest.error);
                OnWordReady?.Invoke(null);
            }
        }
    }
}