
local BasePanel = class("BasePanel")

function BasePanel:MapNode(cfg)
	for k,v in pairs(cfg) do
		local tf = self.transform:Find(v.path)
		if tf then
			-- tf.gameObject
		end
	end
end

return BasePanel