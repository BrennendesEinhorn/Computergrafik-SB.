using System;
using System.Numerics;

namespace DMS.Geometry
{
	public static partial class Meshes
	{
		public static void SetConstantUV(this Mesh mesh, Vector2 uv)
		{
			var uvs = mesh.uv.List;
			uvs.Capacity = mesh.position.List.Count;
			//overwrite existing
			for(int i = 0; i < uvs.Count; ++i)
			{
				uvs[i] = uv;
			}
			//add
			for(int i = uvs.Count; i < mesh.position.List.Count; ++i)
			{
				uvs.Add(uv);
			}
		}

		public static Mesh Clone(this Mesh m)
		{
			var mesh = new Mesh();
			mesh.position.List.AddRange(m.position.List);
			mesh.normal.List.AddRange(m.normal.List);
			mesh.uv.List.AddRange(m.uv.List);
			mesh.IDs.AddRange(m.IDs);
			return mesh;
		}

		public static void Add(this Mesh a, Mesh b)
		{
			var count = (uint)a.position.List.Count;
			a.position.List.AddRange(b.position.List);
			a.normal.List.AddRange(b.normal.List);
			a.uv.List.AddRange(b.uv.List);
			foreach(var id in b.IDs)
			{
				a.IDs.Add(id + count);
			}
		}

		public static Mesh Transform(this Mesh m, Matrix4x4 transform)
		{
			var mesh = new Mesh();
			mesh.uv.List.AddRange(m.uv.List);
			mesh.IDs.AddRange(m.IDs);
			foreach (var pos in m.position.List)
			{
				var newPos = Vector3.Transform(pos, transform);
				mesh.position.List.Add(newPos);
			}
			foreach (var n in m.normal.List)
			{
				var newN = Vector3.TransformNormal(n, transform);
				mesh.normal.List.Add(newN);
			}
			return mesh;
		}

		public static Mesh SwitchHandedness(this Mesh m)
		{
			var mesh = new Mesh();
			mesh.uv.List.AddRange(m.uv.List);
			mesh.IDs.AddRange(m.IDs);
			foreach (var pos in m.position.List)
			{
				var newPos = pos;
				newPos.Z = -newPos.Z;
				mesh.position.List.Add(newPos);
			}
			foreach (var n in m.normal.List)
			{
				var newN = n;
				newN.Z = -newN.Z;
				mesh.normal.List.Add(newN);
			}
			return mesh;
		}

		public static Mesh FlipNormals(this Mesh m)
		{
			var mesh = new Mesh();
			mesh.position.List.AddRange(m.position.List);
			mesh.uv.List.AddRange(m.uv.List);
			mesh.IDs.AddRange(m.IDs);
			foreach (var n in m.normal.List)
			{
				var newN = -n;
				mesh.normal.List.Add(newN);
			}
			return mesh;
		}

		public static Mesh SwitchTriangleMeshWinding(this Mesh m)
		{
			var mesh = new Mesh();
			mesh.position.List.AddRange(m.position.List);
			mesh.normal.List.AddRange(m.normal.List);
			mesh.uv.List.AddRange(m.uv.List);
			for (int i = 0; i < m.IDs.Count; i += 3)
			{
				mesh.IDs.Add(m.IDs[i]);
				mesh.IDs.Add(m.IDs[i + 2]);
				mesh.IDs.Add(m.IDs[i + 1]);
			}
			return mesh;
		}

		public static Mesh CreateCornellBox(float roomSize = 2, float sphereRadius = 0.3f, float cubeSize = 0.6f)
		{
			Mesh mesh = new Mesh();
			var plane = Meshes.CreateQuad(roomSize, roomSize, 2, 2);
			
			var xform = new Transformation();
			xform.TranslateGlobal(0, -roomSize / 2, 0);
			plane.SetConstantUV(new Vector2(3, 0));
			mesh.Add(plane.Transform(xform));
			xform.RotateZGlobal(90f);
			plane.SetConstantUV(new Vector2(1, 0));
			mesh.Add(plane.Transform(xform));
			xform.RotateZGlobal(90f);
			plane.SetConstantUV(new Vector2(0, 0));
			mesh.Add(plane.Transform(xform));
			xform.RotateZGlobal(90f);
			plane.SetConstantUV(new Vector2(2, 0));
			mesh.Add(plane.Transform(xform));
			xform.RotateYGlobal(270f);
			plane.SetConstantUV(new Vector2(0, 0));
			mesh.Add(plane.Transform(xform));

			var sphere = Meshes.CreateSphere(sphereRadius, 4);
			sphere.SetConstantUV(new Vector2(3, 0));
			xform.Reset();
			xform.TranslateGlobal(0.4f, -1 + sphereRadius, -0.2f);
			mesh.Add(sphere.Transform(xform));

			var cube = Meshes.CreateCubeWithNormals(cubeSize);
			cube.SetConstantUV(new Vector2(3, 0));
			xform.Reset();
			xform.RotateYGlobal(35f);
			xform.TranslateGlobal(-0.5f, -1 + 0.5f * cubeSize, 0.1f);
			mesh.Add(cube.Transform(xform));
			return mesh;
		}
		public struct CornellBoxMaterial //use 16 byte alignment or you have to query all variable offsets
		{
			public Vector3 color;
			public float shininess;
		};
		public static CornellBoxMaterial[] CreateCornellBoxMaterial()
		{
			var materials = new CornellBoxMaterial[4];
			materials[0].color = new Vector3(1, 1, 1);
			materials[0].shininess = 0;
			materials[1].color = new Vector3(0, 1, 0);
			materials[1].shininess = 0;
			materials[2].color = new Vector3(1, 0, 0);
			materials[2].shininess = 0;
			materials[3].color = new Vector3(1, 1, 1);
			materials[3].shininess = 256;
			return materials;
		}

		public static Mesh CreateCube(float size = 1.0f)
		{
			float s2 = size * 0.5f;
			var mesh = new Mesh();

			//corners
			mesh.position.List.Add(new Vector3(s2, s2, -s2)); //0
			mesh.position.List.Add(new Vector3(s2, s2, s2)); //1
			mesh.position.List.Add(new Vector3(-s2, s2, s2)); //2
			mesh.position.List.Add(new Vector3(-s2, s2, -s2)); //3
			mesh.position.List.Add(new Vector3(s2, -s2, -s2)); //4
			mesh.position.List.Add(new Vector3(-s2, -s2, -s2)); //5
			mesh.position.List.Add(new Vector3(-s2, -s2, s2)); //6
			mesh.position.List.Add(new Vector3(s2, -s2, s2)); //7

			//Top Face
			mesh.IDs.Add(0);
			mesh.IDs.Add(2);
			mesh.IDs.Add(1);
			mesh.IDs.Add(0);
			mesh.IDs.Add(3);
			mesh.IDs.Add(2);
			//Bottom Face
			mesh.IDs.Add(4);
			mesh.IDs.Add(6);
			mesh.IDs.Add(5);
			mesh.IDs.Add(4);
			mesh.IDs.Add(7);
			mesh.IDs.Add(6);
			//Front Face
			mesh.IDs.Add(1);
			mesh.IDs.Add(6);
			mesh.IDs.Add(7);
			mesh.IDs.Add(1);
			mesh.IDs.Add(2);
			mesh.IDs.Add(6);
			//Back Face
			mesh.IDs.Add(0);
			mesh.IDs.Add(5);
			mesh.IDs.Add(3);
			mesh.IDs.Add(0);
			mesh.IDs.Add(4);
			mesh.IDs.Add(5);
			//Left face
			mesh.IDs.Add(2);
			mesh.IDs.Add(5);
			mesh.IDs.Add(6);
			mesh.IDs.Add(2);
			mesh.IDs.Add(3);
			mesh.IDs.Add(5);
			//Right face
			mesh.IDs.Add(1);
			mesh.IDs.Add(4);
			mesh.IDs.Add(0);
			mesh.IDs.Add(1);
			mesh.IDs.Add(7);
			mesh.IDs.Add(4);
			return mesh;
		}

		public static Mesh CreateCubeWithNormals(float size = 1.0f)
		{
			float s2 = size * 0.5f;
			var mesh = new Mesh();

			//corners
			var c = new Vector3[] {
				new Vector3(s2, s2, -s2),
				new Vector3(s2, s2, s2),
				new Vector3(-s2, s2, s2),
				new Vector3(-s2, s2, -s2),
				new Vector3(s2, -s2, -s2),
				new Vector3(-s2, -s2, -s2),
				new Vector3(-s2, -s2, s2),
				new Vector3(s2, -s2, s2),
			};

			uint id = 0;
			var n = -Vector3.UnitX;

			Action<int> Add = (int pos) => { mesh.position.List.Add(c[pos]); mesh.normal.List.Add(n); mesh.IDs.Add(id); ++id; };

			//Left face
			Add(2);
			Add(5);
			Add(6);
			Add(2);
			Add(3);
			Add(5);
			//Right face
			n = Vector3.UnitX;
			Add(1);
			Add(4);
			Add(0);
			Add(1);
			Add(7);
			Add(4);
			//Top Face
			n = Vector3.UnitY;
			Add(0);
			Add(2);
			Add(1);
			Add(0);
			Add(3);
			Add(2);
			//Bottom Face
			n = -Vector3.UnitY;
			Add(4);
			Add(6);
			Add(5);
			Add(4);
			Add(7);
			Add(6);
			//Front Face
			n = Vector3.UnitZ;
			Add(1);
			Add(6);
			Add(7);
			Add(1);
			Add(2);
			Add(6);
			//Back Face
			n = -Vector3.UnitZ;
			Add(0);
			Add(5);
			Add(3);
			Add(0);
			Add(4);
			Add(5);
			return mesh;
		}

		public static Mesh CreateSphere(float radius_ = 1.0f, uint subdivision = 1)
		{
			//idea: subdivide icosahedron
			const float X = 0.525731112119133606f;
			const float Z = 0.850650808352039932f;

			var vdata = new float[12,3] {
				{ -X, 0.0f, Z}, { X, 0.0f, Z}, { -X, 0.0f, -Z }, { X, 0.0f, -Z },
				{ 0.0f, Z, X }, { 0.0f, Z, -X }, { 0.0f, -Z, X }, { 0.0f, -Z, -X },
				{ Z, X, 0.0f }, { -Z, X, 0.0f }, { Z, -X, 0.0f }, { -Z, -X, 0.0f }
			};
			var tindices = new uint[20,3] {
				{ 0, 4, 1 }, { 0, 9, 4 }, { 9, 5, 4 }, { 4, 5, 8 }, { 4, 8, 1 },
				{ 8, 10, 1 }, { 8, 3, 10 }, { 5, 3, 8 }, { 5, 2, 3 }, { 2, 7, 3 },
				{ 7, 10, 3 }, { 7, 6, 10 }, { 7, 11, 6 }, { 11, 0, 6 }, { 0, 1, 6 },
				{ 6, 1, 10 }, { 9, 0, 11 }, { 9, 11, 2 }, { 9, 2, 5 }, { 7, 2, 11 } };

			Mesh mesh = new Mesh();
			for (int i = 0; i < 12; ++i)
			{
				var p = new Vector3(vdata[i, 0], vdata[i, 1], vdata[i, 2]);
				mesh.normal.List.Add(p);
				mesh.position.List.Add(p);
			}
			for (int i = 0; i < 20; ++i)
			{
				Subdivide(mesh, tindices[i, 0], tindices[i, 1], tindices[i, 2], subdivision);
			}

			//scale
			for (int i = 0; i < mesh.position.List.Count; ++i)
			{
				mesh.position.List[i] *= radius_;
			}

			return mesh.SwitchTriangleMeshWinding();
		}

		public static Mesh CreateIcosahedron(float radius)
		{
			return CreateSphere(radius, 0);
		}

		public static Mesh CreateQuad(float sizeX, float sizeZ, uint segmentsX, uint segmentsZ)
		{
			float deltaX = (1.0f / segmentsX) * sizeX;
			float deltaZ = (1.0f / segmentsZ) * sizeZ;
			Mesh m = new Mesh();
			//add vertices
			for (uint u = 0; u < segmentsX + 1; ++u)
			{
				for (uint v = 0; v < segmentsZ + 1; ++v)
				{
					float x = -sizeX / 2.0f + u * deltaX;
					float z = -sizeZ / 2.0f + v * deltaZ;
					m.position.List.Add(new Vector3(x, 0.0f, z));
					m.normal.List.Add(Vector3.UnitY);
					m.uv.List.Add(new Vector2(u, v));
				}
			}
			uint verticesZ = segmentsZ + 1;
			//add ids
			for (uint u = 0; u < segmentsX; ++u)
			{
				for (uint v = 0; v < segmentsZ; ++v)
				{
					m.IDs.Add(u* verticesZ + v);
					m.IDs.Add(u* verticesZ + v + 1);
					m.IDs.Add((u + 1) * verticesZ + v);

					m.IDs.Add((u + 1) * verticesZ + v);
					m.IDs.Add(u* verticesZ + v + 1);
					m.IDs.Add((u + 1) * verticesZ + v + 1);
				}
			}
			return m;
		}

		private static uint CreateID(Mesh m, uint id1, uint id2)
		{
			//todo: could detect if edge already calculated and reuse....
			uint i12 = (uint)m.position.List.Count;
			Vector3 v12 = m.position.List[(int)id1] + m.position.List[(int)id2];
			v12 /= v12.Length();
			m.normal.List.Add(v12);
			m.position.List.Add(v12);
			return i12;
		}

		private static void Subdivide(Mesh m, uint id1, uint id2, uint id3, uint depth)
		{
			if (0 == depth)
			{
				m.IDs.Add(id1);
				m.IDs.Add(id2);
				m.IDs.Add(id3);
				return;
			}
			uint i12 = CreateID(m, id1, id2);
			uint i23 = CreateID(m, id2, id3);
			uint i31 = CreateID(m, id3, id1);

			Subdivide(m, id1, i12, i31, depth - 1);
			Subdivide(m, id2, i23, i12, depth - 1);
			Subdivide(m, id3, i31, i23, depth - 1);
			Subdivide(m, i12, i23, i31, depth - 1);
		}
	}
}
