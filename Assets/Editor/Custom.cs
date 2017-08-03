using System;
using UnityEditor;
using LuaFramework;

public class Custom
{

	[MenuItem("Custom/Android/Monitor",false,15)]
	public static void BuildMonitorAndroid()
	{
		//BuildLuaResource.BuildLua(false,BuildTarget.Android);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android,"USE_ASSETBUNDLE");
		PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.StandaloneWindows,false);
		UnityEngine.Rendering.GraphicsDeviceType[] api = new UnityEngine.Rendering.GraphicsDeviceType[3];
		api[1] = UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2;
		api[1] = UnityEngine.Rendering.GraphicsDeviceType.Direct3D11;
		api[1] = UnityEngine.Rendering.GraphicsDeviceType.Direct3D9;
		PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows,api);
	}

	[MenuItem("Custom/Android/Editor",false,15)]
	public static void BuildMonitorEditor()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android,"");
		PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.StandaloneWindows,true);
	}

	[MenuItem("Custom/Mode/NONE",false,15)]
	public static void SetModeNone()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup(Util.GetBuildTargetGroup (),"");
	}

	[MenuItem("Custom/Mode/USE_ASSETBUNDLE",false,15)]
	public static void SetModeUseAssetBundle()
	{
		BuildTargetGroup group = Util.GetBuildTargetGroup ();
		string def = PlayerSettings.GetScriptingDefineSymbolsForGroup (group);
		def += "USE_ASSETBUNDLE;";
		PlayerSettings.SetScriptingDefineSymbolsForGroup(group,def);
	}

	[MenuItem("Custom/Mode/ASYNC_MODE",false,15)]
	public static void SetModeAsync()
	{
		BuildTargetGroup group = Util.GetBuildTargetGroup ();
		string def = PlayerSettings.GetScriptingDefineSymbolsForGroup (group);
		def += "ASYNC_MODE;";
		PlayerSettings.SetScriptingDefineSymbolsForGroup(group,def);
	}
}

