#version 430 core

in vec3 v_pos;
in vec3 v_n;
in vec2 v_uv;

out vec4 color; //todo: asdf

void main() 
{
	vec3 light = vec3(0,1,0);
	vec3 normal = normalize(v_n);

	float lambert = max(0.2, dot(normal, light));
	vec3 diffuse = vec3(v_uv * lambert, 0);

	color =  vec4(diffuse, 1);
}