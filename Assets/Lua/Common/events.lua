
Event = {}

local listeners = {}

local traceback = function() 
	return debug.traceback() 
end

function Event.AddListener(name, func, target)
	local listener = listeners[name]
	if listener == nil then
		listener = {}
		listeners[name] = listener
	end	
	table.insert(listener,{func=func,target=target})
end

function Event.RemoveListener(name, func, target)
	local listener = listeners[name]
	if not listener then return end
	for i=#listener,1,-1 do
		local exist = listener[i]
		if exist.func==func and exist.target==target then
			table.remove(listener,i)
			break
		end
	end
end

function Event.Brocast(name, ...)
	local listener = listeners[name]
	if not listener then return end
	for _,exist in ipairs(listener) do
		if exist.target then
			xpcall(function() exist.func(exist.target,unpack(arg)) end,traceback)
		else
			xpcall(function() exist.func(unpack(arg)) end,traceback)
		end
	end
end

return Event