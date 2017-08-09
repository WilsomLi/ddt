
local UIConst = require "Logic/UI/UIConst"

UIManager = {}

local this = UIManager
local uiRoot
local need_open_queue = {}
local exist_panels = {}
local loading_panels = {}

local function ShowPanel(data)
	local exist = exist_panels[data.info.name]
	if not exist then
		return
	end
	local panel = exist.panel
	local args = data.args
	panel.gameObject:SetActive(true)
	
	if panel.ShowPanel then
		panel.ShowPanel(args)
	end
	
	if #need_open_queue > 0 then
		LoadPanel(open_queue[1])
	end
end
	
local function LoadPanel(data)
	local name = data.info.name
	local loading = loading_panels[name]
	if loading then
		loading.args = data.args
		return
	end
	
	loading_panels[name] = data	
	resMgr:LoadPrefab(name,{name},OnLoadPanelFinish)
end

local function OnLoadPanelFinish(objs)
	local go = newObject(objs[0]);
	go.transform:SetParent(uiRoot);
	go.transform.localScale = Vector3.one;
	go.transform.localPosition = Vector3.zero;

	go:AddComponent(typeof(LuaFramework.LuaBehaviour))
	
	local data = loading_panels[go.name]
	ShowPanel(data)
	loading_panels[go.name] = nil
end

local function CheckLoaded(info)
	local name = info.name
	reuturn exist_panels[name] and exist_panels[name].panel
end
------------------------------------------------------------------------

function UIManager.Init()
	uiRoot = GameObject.Find("/Canvas/GuiCamera").transform
end

function UIManager.OpenPanel(info,...)
	print("------UIManager.OpenPanel",info.name)
	local data = {
		data.info = info,
		data.args = arg,
	}
	table.insert(need_open_queue,data)
	
	local first_data = need_open_queue[1]
	if CheckLoaded(first_data.info) then
		table.remove(open_queue,1)
		ShowPanel(first_data)
		return
	end
	LoadPanel(first_data)
end

function UIManager.ClosePanel(info)
	print("------UIManager.ClosePanel",info.name)
	local panel = exist_panels[info.name]
	if panel then
		panel:Close()
	end
end

return this
