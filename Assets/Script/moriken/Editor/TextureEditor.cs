using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CreateRampTexture))] //�g������N���X���w��
public class TextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //����Inspector������\��
        base.OnInspectorGUI();

        //target��ϊ����đΏۂ��擾
        CreateRampTexture Script = target as CreateRampTexture;

        //�{�^����\��
        if (GUILayout.Button("Convert"))
        {
            var texture = CreateTempTexture(Script.width, Script.heigth, Script._gradient);
            // ������Ă΂Ȃ��ƐF���������܂�Ȃ�
            texture.Apply();

            System.IO.File.WriteAllBytes(Application.dataPath + "/temp.png", texture.EncodeToPNG());
            // �폜��Y��Ȃ�
            DestroyImmediate(texture);
            AssetDatabase.Refresh();
        }
    }

    private static Texture2D CreateTempTexture(int width, int height, Gradient gradient = default)
    {
        var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

        float inv = 1f / (width - 1);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                var t = x * inv;
                Color color = gradient.Evaluate(t);
                texture.SetPixel(x, y, color);
            }
        }
        return texture;
    }
}
