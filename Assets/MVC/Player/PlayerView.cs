using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField]
    GameObject Player;
    [SerializeField]
    LineRenderer PlayerVisualizer;
    [SerializeField]
    GameObject Laser;
    [SerializeField]
    ParticleSystem DestroyEffect;

    [SerializeField]
    InfoController InfoController;

    public float IsInvincibilityFrenq = 4;
    float _chargeReloadTimeLerp;

    public void SetLives(int count)
    {
        InfoController.SetLives(count);
    }
    public void SetPlayerTransform(float X, float Y, float Rotation)
    {
        Player.transform.position = new Vector3(X, Y, 0);
        Player.transform.rotation = Quaternion.AngleAxis(Rotation, Vector3.forward);
    }
    public void SpawnPlayer()
    {
        Player.SetActive(true);

    }
    public void PlayerDie()
    {
        Player.SetActive(false);
        StartCoroutine(PlayDieEffect(Player.transform.position));
    }
    IEnumerator PlayDieEffect(Vector2 position)
    {
        DestroyEffect.transform.position = position;
        DestroyEffect.gameObject.SetActive(true);
        DestroyEffect.Play();
        yield return new WaitForSeconds(1);
        DestroyEffect.gameObject.SetActive(false);
        DestroyEffect.Stop();
        yield break;
    }

    public void SetLaserCharges(int laserCharges)
    {
        InfoController.SetLaserCharges(laserCharges);
    }
    public void SetLaserReload(float reloadTime, float maxReloadTime)
    {
        if (_chargeReloadTimeLerp < reloadTime)
            _chargeReloadTimeLerp = reloadTime;
        _chargeReloadTimeLerp = Mathf.Lerp(_chargeReloadTimeLerp, reloadTime, Time.deltaTime);
        InfoController.SetLaserReload(_chargeReloadTimeLerp, maxReloadTime);
    }

    public void SetPlayerInfo(float x, float y, float speedX, float speedY)
    {
        InfoController.SetPlayerInfo(x, y, speedX, speedY);
    }
    public void SetIsInvincibility(bool isInvincibility)
    {
        if (isInvincibility)
        {
            float IsInvincibilityFrenqTime = 1f / IsInvincibilityFrenq;
            PlayerVisualizer.enabled = Time.time % IsInvincibilityFrenqTime > (IsInvincibilityFrenqTime / 2);
        }
        else if (!PlayerVisualizer.enabled)
        {
            PlayerVisualizer.enabled = true;
        }
    }
}
