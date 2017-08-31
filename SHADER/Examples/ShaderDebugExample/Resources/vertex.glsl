#version 430 core				
				out vec3 pos;
void main() {
    const vec3 vertices[4] = vec3[4](vec3(-0.9, -0.8, 0.5),
                                    vec3( 0.9, -0.9, 0.5),
                                    vec3( 0.9,  0.8, 0.5),
                                    vec3(-0.9,  0.9, 0.5));
    pos = vertices[gl_VertexID];
    gl_Position = vec4(pos, 1.0);
}
