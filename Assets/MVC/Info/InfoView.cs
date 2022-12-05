using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoView : MonoBehaviour
{
    [SerializeField]
    TMP_TextArgumentExtention Score;
    [SerializeField]
    TMP_TextArgumentExtention LaserCharges;
    [SerializeField]
    TMP_TextArgumentExtention ShipInfo;
    [SerializeField]
    SliderRaid LaserReload;
    [SerializeField]
    LivesController Lives;
    public void SetLives(int count)
    {
        Lives.SetLives(count);
    }
    public void SetScore(int score)
    {
        Score.SetArguments(("Score", score));
    }
    public void SetLaserCharges(int laserCharges)
    {
        LaserCharges.SetArguments(("laserCharges", laserCharges));
    }
    public void SetLaserReload(float reloadTime, float maxReloadTime)
    {
        LaserReload.value = reloadTime / maxReloadTime;
    }
    public void SetPlayerInfo(float x, float y, float speedX, float speedY)
    {
        ShipInfo.SetArguments(
            ("shipX", x.ToString("N2")),
            ("shipY", y.ToString("N2")),
            ("speedX", speedX.ToString("N2")),
            ("speedY", speedY.ToString("N2"))
            );
    }
}
