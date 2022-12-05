using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Instances")]
    public GameObject Player;
    public GameObject Laser;

    public InfoController InfoController;
    
    public void SetLives(int count)
    {
        InfoController.SetLives(count);
    }
    public void SetPlayerTransform(float X, float Y, float Rotation)
    {
        Player.transform.position = new Vector3(X, Y, 0);
        Player.transform.rotation = Quaternion.AngleAxis(Rotation, Vector3.forward);
    }
}
