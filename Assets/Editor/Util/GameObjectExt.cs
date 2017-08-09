
public class GameObjectExt
{
  [MenuItem("GameObject/复制GameObject路径",false,10)]
  static void CopyGameObjectPath()
  {
    Transform tf = Selection.activeGameObject.transform;
    List<Transform> tfList = new List<Transform>();
    while(tf != null) {
      tfList.Add(tf);
      tf = tf.parent;
    }
    string path = "";
    if(tfList.Count > 2) {
      for(int i=0; i<tfList.Count-2; i++) {
        path = tfList[i].name + "/" + path;
      }
    }
    path = path.Substring(0,path.Length-1);
    EditorGUIUtility.systemCopyBuffer = path;
    Debug.Log("GameObject path:"+path);
  }
}
