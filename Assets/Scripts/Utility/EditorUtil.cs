#if UNITY_EDITOR
using System;
using UnityEditor;

public class EditorUtil
{
	public EditorUtil ()
	{
	}
		
	public static string GetAssetPath(string fileName) {
		int index = fileName.LastIndexOf(".");
		string name = fileName.Substring(0,index);
		string[] guids = AssetDatabase.FindAssets (name);
		for(int i=0; i<guids.Length; i++) {
			string path = AssetDatabase.GUIDToAssetPath(guids[i]);
			if(path.Substring(path.LastIndexOf("/")+1) == fileName) {
				return path;
			}
		}
		return fileName;
	}

	public static BuildTargetGroup GetBuildTargetGroup() {
		BuildTargetGroup group = BuildTargetGroup.Standalone;

		BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
		switch (target) {
		case BuildTarget.Android:
			group = BuildTargetGroup.Android;
			break;
		case BuildTarget.iOS:
			group = BuildTargetGroup.iOS;
			break;
		default:
			break;
		}
		return group;
	}

}

#endif
