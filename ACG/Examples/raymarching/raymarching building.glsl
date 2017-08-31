#version 330

#include "../libs/camera.glsl"
#include "../libs/operators.glsl"

uniform vec2 iResolution;

const float epsilon = 0.0001;
const int maxSteps = 512;

float distField(vec3 point)
{
	float dist1 = sBox(point, vec3(0, 0, 0), vec3(10, 10, 10));
	vec3 repXY = opRepeatCentered(point, vec3(.5, .5, 1));
	float dist2 = sBox(repXY, vec3(0, 0, 0), vec3(0.1, 0.1, 10));
	vec3 repYZ = opRepeatCentered(point, vec3(1, .5, .5));
	float dist3 = sBox(repYZ, vec3(0, 0, 0), vec3(10, 0.1, 0.1));
	return opDifference(dist1, opUnion(dist2, dist3));
}

float ambientOcclusion(vec3 point, float delta, int samples)
{
	vec3 normal = getNormal(point, 0.0001);
	float occ = 0;
	for(int i = 1; i < samples; ++i)
	{
		occ += (i * delta - distField(point + i * delta * normal));
	}
	// occ /= samples;
	return 1 - occ;
}

void main()
{
	vec3 camP = calcCameraPos();
	camP.x += 11.0;
	camP.z += -12.0;
	vec3 camDir = calcCameraRayDir(80.0, gl_FragCoord.xy, iResolution);

	//start point is the camera position
	vec3 point = camP; 	
	bool objectHit = false;
	float t = 0.0;
	//step along the ray 
	int steps = 0;
    for(; (steps < maxSteps)&& (t < 10); ++steps)
    {
		//check how far the point is from the nearest surface
        float dist = distField(point);
		//if we are very close
        if(epsilon > dist)
        {
			objectHit = true;
            break;
        }
		//screen error decreases with distance
		// dist = max(dist, t * 0.001);
		//not so close -> we can step at least dist without hitting anything
		t += dist;

		//calculate new point
        point = camP + t * camDir;
    }

	float effort = steps;
	effort /= maxSteps;
	vec3 red = vec3(1, 0, 0);
	vec3 green = vec3(0, 1, 0);
	vec3 color = mix(green, red, effort);
	if(objectHit)
	{
		vec3 normal = getNormal(point, 0.01);
		vec3 lightDir = normalize(vec3(1, -1.0, 1));
		vec3 toLight = -lightDir;
		float diffuse = max(0, dot(toLight, normal));
		vec3 material = vec3(1); //white
		vec3 ambient = vec3(0.2);

		color = ambient + diffuse * material;	
		color = ambientOcclusion(point, 0.02, 5) * color;
	}
	else
	{
		color = vec3(0);
	}
	gl_FragColor = vec4(color, 1);
}