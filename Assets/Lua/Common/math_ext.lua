
--数学相关辅助函数

local math = math

function math.round(num)
	return math.floor(num + 0.5)
end

function math.int(x)
	return math.modf(x)
end

function math.clamp(value,min,max)
	if value < min then
		return min
	elseif value > max then
		return max
	else
		return value
	end
end
