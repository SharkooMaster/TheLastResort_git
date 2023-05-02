using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{
    public int currentMission = 0;
    public List<Mission> missions = new List<Mission>();

    [Header("UI")]
    [SerializeField] private TMPro.TMP_Text mission_txt;

    private void Start()
    {
        mission_txt.text = missions[currentMission]._name;
        missions[currentMission]._enabled = true;
        missions[currentMission]._stat = Mission.status.ON;
    }

    private void Update()
    {
        if(!missions[currentMission]._enabled)
        {
            missions[currentMission]._enabled = false;
            missions[currentMission]._stat = Mission.status.DONE;
            currentMission++;
            mission_txt.text = missions[currentMission]._name;
            missions[currentMission]._enabled = true;
            missions[currentMission]._stat = Mission.status.ON;
        }
    }
}
