#version 330

uniform vec2 iResolution;
uniform float iGlobalTime;

float smoothQuad(vec2 coord, vec2 lowerLeft, vec2 size, float smoothness)
{
	vec2 a = smoothstep(lowerLeft  - 0.5 * smoothness, lowerLeft  + 0.5 * smoothness, coord);
	vec2 b = 1 - smoothstep(lowerLeft + size  - 0.5 * smoothness, lowerLeft + size  + 0.5 * smoothness, coord);
	return a.x * b.x * a.y * b.y;
}

void main() {
	//coordinates in range [0,1]
    vec2 coord01 = gl_FragCoord.xy/iResolution;
	vec2 coordAspect = coord01;
	coordAspect.x *= iResolution.x / iResolution.y;
	vec2 coord = fract(coordAspect * 20);
	float blue = smoothQuad(coord, vec2(0.25, 0.25), vec2(0.5, 0.5), 0.2);

    vec3 color = vec3(blue);
	
    gl_FragColor = vec4(color, 1.0);
}
