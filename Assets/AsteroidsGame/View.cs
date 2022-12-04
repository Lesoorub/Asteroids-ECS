using UnityEngine;

public abstract class View<T> : MonoBehaviour
{
    public abstract void From(T model);
}
