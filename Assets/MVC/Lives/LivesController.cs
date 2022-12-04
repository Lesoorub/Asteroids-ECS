using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    public LivesView view;
    public LivesModel model;

    private void Start()
    {
        UpdateLives();
    }

    private void OnValidate()
    {
        UpdateLives();
    }

    void UpdateLives()
    {
        view.SetLives(model.Lives);
    }

    public void SetLives(int count)
    {
        model.Lives = count;
        UpdateLives();
    }
}
