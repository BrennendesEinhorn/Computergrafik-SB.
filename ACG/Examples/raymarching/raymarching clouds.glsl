#version 330

#include "../libs/camera.glsl"
#include "../libs/operators.glsl"
#include "../libs/Noise3D.glsl"

uniform vec2 iResolution;
uniform float iGlobalTime;

const float epsilon = 0.0001;
const int maxSteps = 512;

//fractal Brownian motion
float fBm(vec3 coord) 
{
	int octaves = 3;
    float value = 0;
    float amplitude = 0.5;
	float lacunarity = 2;
	float gain = 0.5;
    for (int i = 0; i < octaves; ++i) {
        value += amplitude * (snoise(coord) + 0.5);
        coord = coord * lacunarity;
        amplitude *= gain;
    }
    return value;
}
			
					
float cloudDensity(vec3 point)
{
	return fBm(point * 2) * 0.3;
}

float sPlane(vec3 point, vec3 normal, float d) {
    return dot(point, normal) - d;
}

float distField(vec3 point)
{
	point.y += sin(point.z - iGlobalTime * 6.0) * cos(point.x - iGlobalTime) * .25; //waves!
	return sPlane(point, vec3(0.0, 1.0, 0.0), -0.5);
}

void main()
{
	vec3 camP = calcCameraPos();
	vec3 camDir = calcCameraRayDir(80.0, gl_FragCoord.xy, iResolution);

	vec4 colorSum = vec4(0);
	float t = 0.01;
	//step along the ray 
    for(int steps = 0; (steps < maxSteps); ++steps)
    {
		//break if nearly opaque
		if(colorSum.a > 0.99) break;
		//calculate new point
        vec3 point = camP + t * camDir;

        float newDensity = cloudDensity(point);
		vec4 newColor = vec4(1, 1, 1, newDensity);
		
		colorSum = mix(newColor, colorSum, colorSum.a);

        if(epsilon > distField(point))
        {
			vec3 lightDir = normalize(vec3(cos(iGlobalTime), 1.0, sin(iGlobalTime)));
			vec3 normal = getNormal(point, 0.001);
			float lambert = max(0.2 ,dot(normal, lightDir));
			newColor = vec4(lambert * vec3(1, 0, 0), 1);
			colorSum = mix(colorSum, newColor, colorSum.a);
			// colorSum = newColor;
            break;
        }
		t += 0.1;
    }
	gl_FragColor = vec4(colorSum);
}