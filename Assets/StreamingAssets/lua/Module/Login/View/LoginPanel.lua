
local BasePanel = require "Logic.UI.BasePanel"

local LoginPanel = class("LoginPanel",BasePanel)

local transform;
local gameObject;

--启动事件--
function LoginPanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;

	self.InitPanel();
	logWarn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function LoginPanel.InitPanel()
	self.btnOpen = transform:Find("Open").gameObject;
	self.gridParent = transform:Find('ScrollView/Grid');
end

--单击事件--
function LoginPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

return LoginPanel