using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CreateRampTexture))] //拡張するクラスを指定
public class TextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        CreateRampTexture Script = target as CreateRampTexture;

        //ボタンを表示
        if (GUILayout.Button("Convert"))
        {
            var texture = CreateTempTexture(Script.width, Script.heigth, Script._gradient);
            // これを呼ばないと色が書き込まれない
            texture.Apply();

            System.IO.File.WriteAllBytes(Application.dataPath + "/temp.png", texture.EncodeToPNG());
            // 削除を忘れない
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
