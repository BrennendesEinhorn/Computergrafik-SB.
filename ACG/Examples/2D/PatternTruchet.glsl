///idea from http://thebookofshaders.com/edit.php#09/marching_dots.frag
#version 330

uniform vec2 iResolution;
uniform float iGlobalTime;

const float PI = 3.1415926535897932384626433832795;
const float TWOPI = 2 * PI;
const float EPSILON = 10e-4;

float triangle(vec2 coord, float smoothness)
{
	return smoothstep(1 - smoothness, 1 + smoothness, coord.x + coord.y);
}

float diagonal(vec2 coord, float smoothness)
{
	return smoothstep(coord.x - smoothness, coord.x, coord.y) - smoothstep(coord.x, coord.x + smoothness, coord.y);
}

float circles(vec2 coord)
{
	float a = 0.4;
	float b = 0.6;
	float len = length(coord);
	float len1 = length(coord - vec2(1));
	return (step(len, b) - step(len, a) ) + (step(len1, b) - step(len1, a) );
}

vec2 rotate2D(vec2 coord, float angle)
{
    mat2 rot =  mat2(cos(angle),-sin(angle), sin(angle),cos(angle));
    return rot * coord;
}

///map coordinates to angles 0°,90°,180°, 270°
float angle(vec2 coord)
{
    float index = trunc(mod(coord.x, 5)) * 3;
    index += trunc(mod(coord.y, 3)) * 7;
	index = 0;
	return trunc(mod(index, 4)) * 0.5 * PI;
}

//truchet style pattern
vec2 truchet(vec2 coord, float scale, float timeScale)
{
	coord *= scale; //zoom
	float angle = angle(coord);
	coord = fract(coord);
	coord -= 0.5;
	coord = rotate2D(coord, angle + TWOPI * timeScale * iGlobalTime);
	coord += 0.5;
	return coord;
}

void main() {
	//coordinates in range [0,1]
    vec2 coord = gl_FragCoord.xy/iResolution;
	
	coord.x *= iResolution.x / iResolution.y; //aspect
	
	coord = truchet(coord, 10, 0.1);
	// coord = truchet(coord, 4, 0.1); //rekursive pattern
	
	float grid = triangle(coord, 0.01);
	// grid = diagonal(coord, 0.05);
	// grid = circles(coord);

	const vec3 white = vec3(1);
	vec3 color = (1 - grid) * white;
		
    gl_FragColor = vec4(color, 1.0);
}
