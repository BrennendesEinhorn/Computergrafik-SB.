## Examples
1. [MinimalShaderExample](Examples/MinimalShaderExample) 
1. [ShaderDebugExample](Examples/ShaderDebugExample)
1. [ShaderDebugDialogExample](Examples/ShaderDebugDialogExample)
1. [GeometryExample](Examples/GeometryExample)
1. [MeshExample](Examples/MeshExample)
1. [InstancingExample](Examples/InstancingExample)
1. [TransformationExample](Examples/TransformationExample)
1. [CameraTransformationExample](Examples/CameraTransformationExample)
1. [CameraExample](Examples/CameraExample)
1. [PhongLightingExample](Examples/PhongLightingExample)
1. [EnvMappingExample](Examples/EnvMappingExample)
1. [RenderToTextureExample](Examples/RenderToTextureExample)
1. [ShadowMappingExample](Examples/ShadowMappingExample)
1. [ParticleSystemExample](Examples/ParticleSystemExample)
1. [PhysicsExample](Examples/PhysicsExample)
1. [SSBOExample](Examples/SSBOExample)

## Best Practices
#### Automatic conversion of the data type of a uniform
**Example:**
app code: `GL.Uniform1(shader.GetUniformLocation("time"), timeSource.Elapsed.TotalSeconds);`
shader code: `uniform float time;`

**Problem:** `timeSource.Elapsed.TotalSeconds` returns a `double`. the shader expects a `float`. 
does not work on for instance intel HW, `uniform float time` stays on `0`.

**Solution:** explicit conversion: `GL.Uniform1(shader.GetUniformLocation("time"), (float)timeSource.Elapsed.TotalSeconds);`
