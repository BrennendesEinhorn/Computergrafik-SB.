#version 330
#include "../libs/camera.glsl"

uniform vec2 iResolution;

const float epsilon = 0.0001;
const int maxSteps = 128;

//repeat the given coordinate point every interval c[axis]
vec3 opRepeat(vec3 point, vec3 c)
{
    return mod(point, c) - 0.5 * c;
}

//M = center of sphere
//P = some point in space
// return normal of sphere when looking from point P
vec3 sphereNormal(vec3 M, vec3 P)
{
	return normalize(P - M);
}

vec3 normalField(vec3 point){
	point = -opRepeat(point, vec3(3, 3, 3));
	return sphereNormal(point, vec3(0, 0, 0));
}

float sSphere(vec3 point, vec3 center, float radius) {
    return length(point - center) - radius;
}

float uSphere(vec3 point, vec3 center, float radius) {
    return max(0.0, sSphere(point, center, radius));
}

float distField(vec3 point)
{
	point = opRepeat(point, vec3(3, 3, 3));
	return sSphere(point, vec3(0, 0, 0), 0.3);
}

//by numerical gradient
vec3 getNormal(vec3 point, float delta)
{
	//get points a little bit to each side of the point
	vec3 right = point + vec3(delta, 0.0, 0.0);
	vec3 left = point + vec3(-delta, 0.0, 0.0);
	vec3 up = point + vec3(0.0, delta, 0.0);
	vec3 down = point + vec3(0.0, -delta, 0.0);
	vec3 behind = point + vec3(0.0, 0.0, delta);
	vec3 before = point + vec3(0.0, 0.0, -delta);
	//calc difference of distance function values == numerical gradient
	vec3 gradient = vec3(distField(right) - distField(left),
		distField(up) - distField(down),
		distField(behind) - distField(before));
	return normalize(gradient);
}

void main()
{
	vec3 camP = calcCameraPos() ;//+ vec3(0, 0, -1);
	vec3 camDir = calcCameraRayDir(80.0, gl_FragCoord.xy, iResolution);

	//start point is the camera position
	vec3 point = camP; 	
	bool objectHit = false;
	float t = 0.0;
	//step along the ray 
    for(int steps = 0; steps < maxSteps; ++steps)
    {
		//check how far the point is from the nearest surface
        float dist = distField(point);
		//if we are very close
        if(epsilon > dist)
        {
			objectHit = true;
            break;
        }
		//not so close -> we can step at least dist without hitting anything
        t += dist;
		//calculate new point
        point = camP + t * camDir;
    }

	if(objectHit)
	{
		// vec3 normal = getNormal(point, 0.01);
		vec3 normal = normalField(point);
		vec3 lightPos = vec3(0);
		float diffuse = max(0, dot(normalize(lightPos - point), normal));
		vec3 color = vec3(1); //white
		vec3 ambient = vec3(0.1);
		gl_FragColor = vec4(ambient + diffuse * color, 1);
	}
	else
	{
		gl_FragColor = vec4(0, 0, 0, 1);
	}
}