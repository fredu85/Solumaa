using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleAudioEvent))]
public class EditorScript : Editor
{ 

    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();
        SimpleAudioEvent myinspectorButton = (SimpleAudioEvent)target;

        if(GUILayout.Button("Raise()"))
        {
            //if (myinspectorButton.thisAudioSource != null)
            //    myinspectorButton.Play(myinspectorButton.thisAudioSource);
            //else
            //    Debug.LogWarning("Yoo, add a AudioSource to play");
          // myinspectorButton.PlayInEditor();
        }
    }
}
