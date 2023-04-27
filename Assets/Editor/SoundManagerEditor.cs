using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    private SerializedProperty bgmProperty;
    private SerializedProperty seProperty;
    private SerializedProperty voiceProperty;

    private void OnEnable()
    {
        bgmProperty = serializedObject.FindProperty("bgm");
        seProperty = serializedObject.FindProperty("se");
        voiceProperty = serializedObject.FindProperty("voice");
    }

    //public override void OnInspectorGUI()
    //{
        //EditorGUI.BeginChangeCheck();

        //for (int i = 0; i < bgmProperty.arraySize; i++)
        //{
        //    SerializedProperty bgmElement = bgmProperty.GetArrayElementAtIndex(i);

        //    SerializedProperty fadeInProperty = bgmElement.FindPropertyRelative("fadeIn");


            //EditorGUILayout.PropertyField(fadeInProperty);
            //if (fadeInProperty.boolValue)
            //{
            //    SerializedProperty fadeTimeProperty = bgmElement.FindPropertyRelative("TimefadeIn");
            //    //EditorGUILayout.PropertyField(fadeTimeProperty);
            //}
            //else
            //{
            //    SerializedProperty fadeTimeProperty = bgmElement.FindPropertyRelative("TimefadeIn");

            //}
        //}



        //EditorGUILayout.PropertyField(bgmProperty, true);




        //serializedObject.ApplyModifiedProperties();
    //}

}