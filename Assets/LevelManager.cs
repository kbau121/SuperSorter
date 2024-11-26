using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static LevelSequence;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private LevelSequence LevelSequence;
    private int EventIndex = 0;
    private float EventTimer = 0f;

    private Dictionary<int, Launcher> Launchers;

    // Temporary Testing Variables
    public int Score { get; private set; } = 0;
    public float TimeElapsed { get; private set; } = 0.0f;

    // Temporary Alert Timer
    // TODO Make an alert event
    private float alertTimer = 0f;
    private float alertOffset = 0.5f;
    private int launchIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // should never trigger
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        Launchers = Object.FindObjectsOfType<Launcher>().ToDictionary(
            (launcher) => { return launcher.LauncherID; },
            (launcher) => { return launcher; });

        SetTimer();
    }

    void Update()
    {
        EventTimer -= Time.deltaTime;
        alertTimer -= Time.deltaTime;
        TimeElapsed += Time.deltaTime;

        if (alertTimer <= 0f) TriggerAlert();
        if (EventTimer <= 0f) TriggerEvent();
    }

    private void TriggerAlert()
    {
        LevelEvent levelEvent = LevelSequence.Events[EventIndex];

        launchIndex = Random.Range(0, levelEvent.LaunchEvents.Length);
        LaunchEvent launchEvent = levelEvent.LaunchEvents[launchIndex];
        if (Launchers.ContainsKey(launchEvent.LauncherID))
        {
            Launcher launcher = Launchers[launchEvent.LauncherID];

            launcher.Alert();
        }
        
        alertTimer = float.PositiveInfinity;
    }

    private void TriggerEvent()
    {
        LevelEvent levelEvent = LevelSequence.Events[EventIndex];

        //int launchIndex = Random.Range(0, levelEvent.LaunchEvents.Length);
        LaunchEvent launchEvent = levelEvent.LaunchEvents[launchIndex];
        if (Launchers.ContainsKey(launchEvent.LauncherID))
        {
            Launcher launcher = Launchers[launchEvent.LauncherID];

            int objectIndex = Random.Range(0, launchEvent.Objects.Count);
            launcher.LaunchObject(
                launchEvent.Objects[objectIndex],
                launchEvent.Target, launchEvent.Strength,
                launchEvent.TargetRadius, launchEvent.StrengthRange
                );
        }

        EventIndex++;
        SetTimer();
    }

    private void SetTimer()
    {
        // A valid event exists
        if (EventIndex < LevelSequence.Events.Length)
        {
            EventTimer = LevelSequence.Events[EventIndex].Delay;
            alertTimer = EventTimer - alertOffset;
            return;
        }

        // The sequence should loop
        if (LevelSequence.DoLoop && LevelSequence.Events.Length > 0)
        {
            EventIndex = 0;
            EventTimer = LevelSequence.Events[EventIndex].Delay;
            alertTimer = EventTimer - alertOffset;
            return;
        }

        // No more valid events exist
        EventTimer = float.PositiveInfinity;
        alertTimer = float.PositiveInfinity;
    }

    public void ModifyScore(bool success)
    {
        Score += success ? 10 : -1;
    }
}
