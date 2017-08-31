#version 430 core

uniform float deltaTime;
uniform int particleCount;

struct Particle
{
	vec2 position;
	vec2 velocity;
	//vec4 color; //make it vec4  not vec3 because of alignment in std430
};

layout(std430) buffer BufferParticle
{
	Particle particle[];
};
 
bool outside(vec2 pos)
{
	return any(greaterThan(abs(pos), vec2(1))); 
}

void updateParticle(inout Particle p)
{
	p.position += p.velocity * deltaTime; //update

	if(outside(p.position))
	{
		p.velocity = -p.velocity;
	}
}

void setPosition(in Particle p)
{
	gl_Position = vec4(p.position, 0.5, 1.0);
}

void main() 
{
	gl_PointSize = 20.0;
	updateParticle(particle[gl_VertexID]);
	for(int i = 0; i < particleCount; ++i)
	{
		if(i == gl_VertexID) continue;
	}
	setPosition(particle[gl_VertexID]);
}