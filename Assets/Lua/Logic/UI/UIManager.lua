
require "Logic/UI/UIConst"

UIManager = {}
local this = UIManager
local uiRoot

function UIManager.Init()
	uiRoot = GameObject.Find("/Canvas/GuiCamera").transform
	-- print_r(uiRoot)
end

function UIManager.OpenPanel(info)
	print("------UIManager.OpenPanel",info.name)
	resMgr:LoadPrefab(info.name,{info.name},this.OnOpenPanel)

end

function UIManager.ClosePanel()

end

function UIManager.OnOpenPanel(objs)
	local go = newObject(objs[0]);
	go.transform:SetParent(uiRoot);
	go.transform.localScale = Vector3.one;
	go.transform.localPosition = Vector3.zero;
    
    go:AddComponent(typeof(LuaFramework.LuaBehaviour))
end