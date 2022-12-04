using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(TMP_TextArgumentExtention))]
public class TMP_TextArgumentExtentionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = target as TMP_TextArgumentExtention;
        if (script.Target == null)
            script.Target = script.GetComponent<TMP_Text>();
        DrawDefaultInspector();
    }
}
#endif

[RequireComponent(typeof(TMP_Text))]
public class TMP_TextArgumentExtention : MonoBehaviour
{
    /// <summary>
    /// Целевой текст в который будет устанавливаться строка по заданному паттерну
    /// </summary>
    [Header("Set in inspector")]
    [Tooltip("Sets automaticly")]
    public TMP_Text Target;
    /// <summary>
    /// Строка в которую будут вставляться передоваемые аргументы
    /// </summary>
    [Multiline]
    public string Pattern = string.Empty;

    void Awake()
    {
        //Автоустановка текста, если его не установли раньше
        if (Target == null)
            Target = GetComponent<TMP_Text>();
        //Автоустановка паттерна
        if (!string.IsNullOrEmpty(Pattern) &&
            Target != null)
            Pattern = Target.text;
    }
    public void SetArguments(params (string argName, object argValue)[] arguments)
    {
        if (Target == null)//Целевой текст должен существовать
            return;

        //Замещение названий аргументов в паттерне передаваемым массивом аргументов
        string newText = Pattern;
        foreach (var arg in arguments)
        {
            newText = newText.Replace($"{{{arg.argName}}}", arg.argValue.ToString());
        }

        Target.text = newText;
    }
}