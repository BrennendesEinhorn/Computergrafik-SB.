#version 330

#include "../libs/camera.glsl"

uniform float iGlobalTime;
uniform vec2 iResolution;
uniform sampler2D tex0;
uniform sampler2D tex1;

const float epsilon = 0.0001;
const vec3 terrainBottomCenter = vec3(0, 0, 0);
const vec3 terrainExtents = vec3(400, 20, 200);

//dot(n, O +t*d)= -k
//dot(n,O) + dot(n, t*d) = -k
//t*dot(n,d)=-k-dot(n,O)
float plane(vec3 n, float k, vec3 O, vec3 d)
{
	float denominator = dot(n, d);
	if(abs(denominator) < epsilon)
	{
		//no intersection
		return -10000.0;
	}
	return (-k-dot(n,O)) / denominator;
}	

void calcMinMaxT(vec3 O, vec3 d, out float minT, out float maxT)
{
	if(abs(dot(d, vec3(0, 1, 0))) < epsilon)
	{
		minT = 0; maxT = 10000; return; //parallel ray
	}
	float top = max(0 ,plane(vec3(0, -1, 0), terrainBottomCenter.y + terrainExtents.y, O, d));
	float bottom = max(0, plane(vec3(0, 1, 0), terrainBottomCenter.y, O, d));
	minT = min(top, bottom);
	maxT = max(top, bottom);
}

bool hitTerrainTop(vec3 ro, vec3 rd)
{
	//only checking top plane
	float d = terrainBottomCenter.y + terrainExtents.y;
	if(ro.y <= d) return true; //below top plane
	vec3 n = vec3(0, 1, 0);
	return 0 < plane(n, d, ro, rd);
}

vec2 toTextureSpace(vec2 p)
{
	vec2 minCorner = terrainBottomCenter.xz - 0.5 * terrainExtents.xz;
	return (p - minCorner) / terrainExtents.xz;
}

float f(vec2 p)
{
	return texture(tex0, toTextureSpace(p)).x * terrainExtents.y + terrainBottomCenter.y;
}

vec3 colorF(vec2 p)
{
	return texture(tex1, toTextureSpace(p)).rgb;
}

float rayMarchingBisection(vec3 ro, vec3 rd, float minT, float maxT, int count)
{
	float t;
	for(int i = 0; i < count; ++i)
	{
		float middle = 0.5 * (minT + maxT);
		vec3 p = ro + rd * middle;
		if( p.y < f( p.xz ) )
		{
			//inside
			maxT = middle;
		}
		else
		{
			//outside
			minT = middle;
		}
		t = middle;
	}
	return t;
}

float rayMarching(vec3 ro, vec3 rd, float minT, float maxT, int maxSteps, out int steps)
{
	steps = 0;
	float delta = maxT / maxSteps;
	for(float t = minT; t < maxT; t += delta)
	{
		++steps;
		vec3 p = ro + t * rd;
		if( p.y < f( p.xz ) )
		{
			//inside
			// return t;
			steps += 5;
			return rayMarchingBisection(ro, rd, t - delta, t, 5);
		}
		// delta += 0.0003;
	}
	return maxT;
}

vec3 getNormal(vec3 p, float delta)
{
    vec3 n;
	n.x = f(vec2(p.x-delta,p.z)) - f(vec2(p.x+delta,p.z));
	n.y = 2.0 * delta;
	n.z = f(vec2(p.x,p.z-delta)) - f(vec2(p.x,p.z+delta));
    return normalize(n);
}

vec3 getShading(vec3 p, vec3 n)
{
	vec3 color = colorF(p.xz);
	vec3 lightDir = vec3(0.0, -1.0, 1.0);
	vec3 l = normalize(-lightDir);
	// return dot(l, n) * color;
	return color;
}

vec3 terrainColor(vec3 ro, vec3 rd, float t)
{
    vec3 p = ro + rd * t;
    vec3 n = getNormal( p, 0.8 );
    vec3 s = getShading( p, n );
    return s;
}

void main()
{
	vec3 camP = calcCameraPos();
	camP.y += 6;
	vec3 camDir = calcCameraRayDir(80.0, gl_FragCoord.xy, iResolution);

	vec3 color = vec3(0, 1, 0);
	float minT, maxT;
	calcMinMaxT(camP, camDir, minT, maxT);
	maxT = min(maxT, 500);
	
	int maxSteps = 1000;
	int steps;
	float t = rayMarching(camP, camDir, 0, maxT, maxSteps, steps);
	color = t < maxT ? terrainColor(camP, camDir, t): vec3(0.0);
	// color = mix(vec3(0, 1, 0), vec3(1, 0, 0), steps / float(maxSteps)); //effort
	// color = vec3(minT / maxT);
	gl_FragColor = vec4(color, 1.0);
}


