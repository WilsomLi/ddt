
local print_r = require "3rd/sproto/print_r"
require "Logic/UI/UIConst"

UIManager = {}

function UIManager.Init()

end

function UIManager.OpenPanel(info)
	print("------UIManager.OpenPanel")
	print_r(info)
end

function UIManager.ClosePanel()

end