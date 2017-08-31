#version 430 core				

uniform mat4 camera;

in vec4 position;
in float fade;

out float fadeFrag;

void main() 
{
	fadeFrag = fade;
	vec4 pos = camera * position;
	gl_PointSize = (1 - pos.z / pos.w) * 1000;
	gl_Position = pos;
}