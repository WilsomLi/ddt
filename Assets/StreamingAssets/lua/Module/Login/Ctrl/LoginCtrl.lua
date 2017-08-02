
local LoginCtrl = class("LoginCtrl")

local message;
local transform;
local gameObject;

--构建函数--
function LoginCtrl.New()
	logWarn("MessageCtrl.New--->>");
	return this;
end

function LoginCtrl.Awake()
	logWarn("MessageCtrl.Awake--->>");
	panelMgr:CreatePanel('Message', this.OnCreate);
end

--启动事件--
function LoginCtrl.OnCreate(obj)
	gameObject = obj;

	message = gameObject:GetComponent('LuaBehaviour');
	message:AddClick(MessagePanel.btnClose, this.OnClick);

	logWarn("Start lua--->>"..gameObject.name);
end

--单击事件--
function LoginCtrl.OnClick(go)
	destroy(gameObject);
end

--关闭事件--
function LoginCtrl.Close()
	panelMgr:ClosePanel(CtrlNames.Message);
end