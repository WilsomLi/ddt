
local BasePanel = class("BasePanel")

function BasePanel:ShowPanel(args)
		
end

functio BasePanel:Close()

end

function BasePanel:MapNode(cfg)
	for k,v in pairs(cfg) do
		local tf = self.transform:Find(v.path)
		if tf then
			local obj = v.type and tf:GetComponent(typeof(v.type)) or tf.gameObject
			if obj then
				if v.onClick then
					
				end
			end			
		end
	end
end

return BasePanel
