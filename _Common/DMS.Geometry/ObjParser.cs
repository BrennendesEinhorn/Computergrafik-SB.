using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace DMS.Geometry
{
	public class ObjParser
	{
		public class Vertex
		{
			public int idNormal;
			public int idPos;
			public int idTexCoord;

			public Vertex(int idPos, int idTexCoord, int idNormal)
			{
				this.idPos = idPos;
				this.idTexCoord = idTexCoord;
				this.idNormal = idNormal;
			}
		}
		
		public string materialFileName;
		public List<Vector3> position = new List<Vector3>();
		public List<Vector3> normals = new List<Vector3>();
		public List<Vector2> texCoords = new List<Vector2>();
		public List<List<Vertex>> faces = new List<List<Vertex>>();

		public ObjParser(byte[] data)
		{
			char[] splitCharacters = new char[] { ' ' };
			string line;
			using (TextReader reader = new StreamReader((new MemoryStream(data))))
			{
				while (!ReferenceEquals(null, (line = reader.ReadLine())))
				{
					line = line.Trim(splitCharacters);
					line = line.Replace("  ", " ");

					string[] parameters = line.Split(splitCharacters);
					switch (parameters[0])
					{
						case "mtllib": //material lib
							materialFileName = parameters[1];
							break;
						case "p": // Point
							break;

						case "v": // Vertex
							float x = float.Parse(parameters[1], CultureInfo.InvariantCulture);
							float y = float.Parse(parameters[2], CultureInfo.InvariantCulture);
							float z = float.Parse(parameters[3], CultureInfo.InvariantCulture);
							position.Add(new Vector3(x, y, z));
							break;

						case "vt": // TexCoord
							float u = float.Parse(parameters[1], CultureInfo.InvariantCulture);
							float v = float.Parse(parameters[2], CultureInfo.InvariantCulture);
							texCoords.Add(new Vector2(u, v));
							break;

						case "vn": // Normal
							float nx = float.Parse(parameters[1], CultureInfo.InvariantCulture);
							float ny = float.Parse(parameters[2], CultureInfo.InvariantCulture);
							float nz = float.Parse(parameters[3], CultureInfo.InvariantCulture);
							Vector3 n = new Vector3(nx, ny, nz);
							normals.Add(n);
							break;

						case "f":
							//todo: add face
							var face = new List<Vertex>();
							faces.Add(face);
							for (int i = 1; i < parameters.Length; ++i)
							{
								face.Add(ParseVertex(parameters[i]));
							}
							break;
						case "g": //new group
							break;
						case "usemtl": //set current material
							break;
					}
				}
			}
		}

		private Vertex ParseVertex(string faceParameter_)
		{
			char[] faceParameterSplitter = new char[] { '/' };
			string[] parameters = faceParameter_.Split(faceParameterSplitter);

			int idPos = ParseID(parameters, 0, position.Count);
			int idTexCoord = ParseID(parameters, 1, texCoords.Count);
			int idNormal = ParseID(parameters, 2, normals.Count);
			return new Vertex(idPos, idTexCoord, idNormal);
		}

		private static int ParseID(IList<string> parameters_, int pos_, int idCount)
		{
			if (parameters_.Count > pos_)
			{
				int index;
				if (int.TryParse(parameters_[pos_], out index))
				{
					if (index < 0) index = idCount + index;
					else index = index - 1;
					return index;
				}
			}
			return -1;
		}
	}
}
