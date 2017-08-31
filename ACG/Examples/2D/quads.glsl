#version 330

uniform vec2 iResolution;
uniform float iGlobalTime;

float quad(vec2 coord, vec2 lowerLeft, vec2 size)
{
	vec2 a = step(lowerLeft, coord);
	vec2 b = 1 - step(lowerLeft + size, coord);
	return a.x * b.x * a.y * b.y;
}

float repeatTent(float x, float width)
{
	return abs(mod(x, width) - 0.5 * width);
}

float repeatStep(float x, float width)
{
	return step(0.5 * width, mod(x, width));
}

float move(float t)
{
	return repeatTent(t, 2.0) * 0.1 * repeatStep(t + 1.0, 4.0);
}

void main() {
	//coordinates in range [0,1]
    vec2 coord01 = gl_FragCoord.xy/iResolution;
	vec2 coordAspect = coord01;
	coordAspect.x *= iResolution.x/iResolution.y;

	vec2 coordRed = coordAspect;
	coordRed.y -= move(iGlobalTime + 2);
	coordRed = mod(coordRed, vec2(0.2, 0.2));
	float red = quad( coordRed, vec2(0.0, 0.0), vec2(0.1, 0.1));
	
	vec2 coordGreen = coordAspect;
	coordGreen.x -= move(0.25 * iGlobalTime + 4);
	coordGreen = mod(coordGreen, vec2(0.2, 0.2));
	float green = quad(coordGreen, vec2(0.0, 0.1), vec2(0.1, 0.1));

	vec2 coordBlue = coordAspect;
	coordBlue.x -= move(0.5 * iGlobalTime);
	coordBlue = mod(coordBlue, vec2(0.2, 0.2));
	float blue = quad(coordBlue, vec2(0.0, 0.1), vec2(0.1, 0.1));

    vec3 color = vec3(red, green, blue);
	
    gl_FragColor = vec4(color, 1.0);
}
