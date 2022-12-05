using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    public InfoView view;
    public InfoModel model;

    public void SetScore(int score)
    {
        model.Score = score;
        view.SetScore(model.Score);
    }
    public void SetLaserCharges(int laserCharges)
    {
        model.LaserCharges = laserCharges;
        view.SetScore(model.LaserCharges);
    }
    public void SetLaserReload(float reloadTime, float maxReloadTime)
    {
        view.SetLaserReload(reloadTime, maxReloadTime);
    }
    public void SetLives(int count)
    {
        view.SetLives(count);
    }
}
