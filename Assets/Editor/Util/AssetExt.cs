
public class AssetExt
{
  [MenuItem("Assets/Find Refrences",false,10)]
  static void FindRefrence()
  {
    string path = AssetDatabase.GetAssetPath(Selection.activeObject);
    if(string.IsNullOrEmpty(path)) {
      return;
     }
     List<string> withoutExt = new List<string>(){".prefab",".unity",".mat",".asset"};
     string[] files = Directory.GetFiles(Application.dataPath,"*.*",SearchOption.AllDirectories).
      Where(s=>withoutExt.Contains(Path.GetExtension(s).ToLower())).ToArray();
      int index = 0;
      int cnt = files.Length;
      
      EditorApplication.update = delegate () {
        string file = files[index];
        bool bCancel = EditorUtility.DisplayCancelableProgressBar("匹配中",file,(float)index/cnt);
        if(Regex.IsMatch(File.ReadAllText(file),guid))
        {
          Debug.Log(file);
        }
        index ++;
        if(bCancel || index >= cnt) {
          EditorUtility.ClearProgressBar();
          EditorApplication.update = null;
          index = 0;
          Debug.Log("匹配结束");
        }
      }
  }
}
