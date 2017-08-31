using DMS.Geometry;
using OpenTK.Graphics.OpenGL;

namespace DMS.OpenGL
{
	public static class VAOLoader
	{
		/// <summary>
		/// Creates a VertexArrayObject from a mesh expecting the MeshAttribute names as shader variable names for the attributes 
		/// </summary>
		/// <param name="mesh">to load to the VertexArrayObject</param>
		/// <param name="shader">for the attribute location bindings</param>
		/// <returns></returns>
		public static VAO FromMesh(Mesh mesh, Shader shader)
		{
			var vao = new VAO();
			if(mesh.position.List.Count > 0) vao.SetAttribute(shader.GetAttributeLocation(mesh.position.Name), mesh.position.List.ToArray(), VertexAttribPointerType.Float, 3);
			if (mesh.normal.List.Count > 0) vao.SetAttribute(shader.GetAttributeLocation(mesh.normal.Name), mesh.normal.List.ToArray(), VertexAttribPointerType.Float, 3);
			if (mesh.uv.List.Count > 0) vao.SetAttribute(shader.GetAttributeLocation(mesh.uv.Name), mesh.uv.List.ToArray(), VertexAttribPointerType.Float, 2);
			vao.SetID(mesh.IDs.ToArray());
			vao.PrimitiveType = PrimitiveType.Triangles;
			return vao;
		}
	}
}
