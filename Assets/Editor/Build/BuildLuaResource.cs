
public class BuildLuaResource
{
  static List<string> m_files = new List<string>();
  
  const string ASSET_FOLDER = "Assets";
  
  
  public static void BuildLua(BuildTarget target)
  {
    m_files.Clear();
    ForeachFolder(AppConst.luaAssetPath);
    CopyOrEncode(true,target);
    AssetDatabase.Refresh();
  }
  
  static void ForeachFolder(string path)
  {
    string[] files = Directory.GetFiles(path);
    string[] dirs = Directory.GetDirectories(path);
    foreach(string file in files) {
     m_files.Add(file) ;
    }
    foreach(string dir in dirs) {
     ForeachFolder(dir) ;
    }
  }
  
  public static void CopyOrEncode(bool isEncode,BuildTarget target)
  {
    string fileStr = "";
    string path = "";
    string newPath = "";      
    
     foreach(stirng file in m_files) {
      if(isEncode) {
        newPath = file.Substring(file.IndexOf(ASSET_FOLDER) + 7);
        fileStr = File.ReadAllText(file,Encoding.UTF8);
        fileStr = XXTea.Encrypt(fileStr,encodeKey);
        File.WriteAllText(newPath,fileStr,Encoding.UTF8);
      }
       else {
         File.Copy(file,newPath,true);
       }
     }
  }

}
