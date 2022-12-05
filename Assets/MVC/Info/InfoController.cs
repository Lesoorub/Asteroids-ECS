using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    public InfoView view;
    public InfoModel model;

    public void SetScore(int score)
    {
        view.SetScore(score);
    }
    public void SetLaserCharges(int laserCharges)
    {
        view.SetLaserCharges(laserCharges);
    }
    public void SetLaserReload(float reloadTime, float maxReloadTime)
    {
        view.SetLaserReload(reloadTime, maxReloadTime);
    }
    public void SetLives(int count)
    {
        view.SetLives(count);
    }
    public void SetPlayerInfo(float x, float y, float speedX, float speedY)
    {
        view.SetPlayerInfo(x, y, speedX, speedY);
    }
}
