
require "Configs.init"

--主入口函数。从这里开始lua逻辑
function Main()					
	print("logic start Main.lua Main()")
	print(#DataConfigs.building)
end

--场景切换通知
function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end