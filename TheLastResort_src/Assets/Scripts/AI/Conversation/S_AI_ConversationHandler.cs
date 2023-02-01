using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class S_AI_ConversationHandler : MonoBehaviour
{
    [Header("Prerequists")]
    string root;
    public string pathPrefixToAudio;
    public string pathToConversations;
    public string myName;
    public string targetName;

    [Header("Personality")]
    [Range(0, 10)] public int talkativeMin;
    [Range(0, 10)] public int talkativeMax;

    [Header("Runtime")]
    public Ai target;
    public bool nearAi;
    public bool conversationRunning;
    public float calloutAwait;

    [Header("Runtime Conversation")]
    public string covnersationID;

    private void OnTriggerEnter(Collider other)
    {
        if (!conversationRunning)
        {
            target = other.gameObject.GetComponent<Ai>();
            if (target)
            {
                targetName = target._name;
                nearAi = true;
            }
        }
    }

    bool replyToMe = false;
    void Update()
    {
        if (nearAi)
        {
            float startConvo = Random.Range(0, 10);
            float replyTime = 0f;
            if (startConvo > talkativeMin && startConvo < talkativeMax && !conversationRunning)
            {
                target.gameObject.GetComponent<S_AI_ConversationHandler>().conversationRunning = true;
                conversationRunning = true;
                chooseConversation();

                triggerCallout_Audio(targetName);
                replyTime = Time.time + calloutAwait;
                replyToMe = true;
            }

            // Target Ai reply to callout
            if (Time.time >= replyTime && replyToMe)
            {
                Debug.Log("Joe replying");
                target.gameObject.GetComponent<S_AI_ConversationHandler>().triggerCalloutReply_Audio(myName);
                replyToMe = false;
            }
        }

        if (currentAudioCue != -1)
        {
            conversate();
        }
    }

    public void triggerCallout_Audio(string target)
    {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(pathToConversations + $"{myName}/Callout_{targetName}_1"), transform.position, 1);
    }

    public void triggerCalloutReply_Audio(string target)
    {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(pathToConversations + $"{myName}/Reply_Yea_1"), transform.position, 1);
    }

    public void triggerConversationClip(string _path)
    {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(pathToConversations + $"{myName}{_path}"), transform.position, 1);
    }

    public void triggerAlertClip(string _path)
    {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(pathToConversations + $"{myName}/Alert/{_path}"), transform.position, 1);
    }
    public void triggerCombatClip(string _path)
    {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(pathToConversations + $"{myName}/Combat/{_path}"), transform.position, 1);
    }

    [Header("JSON Conversations")]
    public string pathToJsonConversations;
    public int jsonConversationsMin;
    public int jsonConversationsMax;
    public Json_Array selectedConvo;
    void chooseConversation()
    {
        string _path = root + pathToJsonConversations + UnityEngine.Random.Range(jsonConversationsMin, jsonConversationsMax).ToString() + ".txt";
        Debug.Log(_path);
        string jsonConvos = File.ReadAllText(_path);
        print(jsonConvos);
        selectedConvo = new Json_Array();
        JsonUtility.FromJsonOverwrite(jsonConvos, selectedConvo);

        startConversation();
    }

    List<float> audioQueue = new List<float>();
    int currentAudioCue = -1;
    private void startConversation()
    {
        foreach (var item in selectedConvo.array)
        {
            audioQueue.Add(item.delay);
        }
        currentAudioCue = 0;
        nextCuo = Time.time + 2;
    }

    float nextCuo;
    private void conversate()
    {
        if (Time.time > nextCuo)
        {
            nextCuo = Time.time + audioQueue[currentAudioCue];
            if (selectedConvo.array[currentAudioCue].id == 0)
            {
                triggerConversationClip(selectedConvo.array[currentAudioCue].path);
            }
            else
            {
                target.gameObject.GetComponent<S_AI_ConversationHandler>().triggerConversationClip(selectedConvo.array[currentAudioCue].path);
            }
            if (currentAudioCue != selectedConvo.array.Count)
                currentAudioCue++;
            else
                currentAudioCue = -1;
        }
    }

    private void Start()
    {
        root = Application.dataPath;
    }
}
