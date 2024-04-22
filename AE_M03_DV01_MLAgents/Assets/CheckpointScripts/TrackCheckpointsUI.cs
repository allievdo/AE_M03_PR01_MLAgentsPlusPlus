using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpointsUI : MonoBehaviour
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;

    public GameObject UI;

    private void Start()
    {
        trackCheckpoints.OnPlayerCorrectCheckpoint += TrackCheckpoints_OnPlayerCorrectCheckpoint;
        trackCheckpoints.OnPlayerWrongCheckpoint += TrackCheckpoints_OnPlayerWrongCheckpoint;

        Hide();
    }

    private void TrackCheckpoints_OnPlayerWrongCheckpoint(object sender, System.EventArgs e)
    {
        Show();
    }

    private void TrackCheckpoints_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e)
    {
        Hide();
    }

    //private void Update()
    //{
    //    if (trackCheckpoints.wrong == true)
    //    {
    //        Show();
    //    }

    //    if (trackCheckpoints.correct == true)
    //    {
    //        Hide();
    //    }
    //}
    private void Show()
    {
        UI.SetActive(true);
    }

    private void Hide()
    {
        UI.SetActive(false);
    }
}
