using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesView : MonoBehaviour
{
    public GameObject LiveImagePrefab;
    public Transform LivesParent;

    public void SetLives(int lives)
    {
        if (LivesParent.childCount == lives) return;

        //Destory all lives images
        for (int k = 0; k < LivesParent.childCount; k++)
            Destroy(LivesParent.GetChild(k).gameObject);

        for (int k = 0; k < lives; k++)
            Instantiate(LiveImagePrefab, LivesParent);
    }
}
