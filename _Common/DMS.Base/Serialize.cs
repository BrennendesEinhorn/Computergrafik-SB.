using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DMS.Base
{
	public static class Serialize
	{
		/// <summary>
		/// Serializes the obj into SOAP format file.
		/// </summary>
		/// <param name="serializableObject">The object to be serialized.</param>
		/// <param name="fileName">The file name_.</param>
		public static void ObjIntoSoapFile(this object serializableObject, string fileName)
		{
			if (ReferenceEquals(null,  serializableObject)) new ArgumentNullException("Null object given!");
			SoapFormatter formatter = new SoapFormatter();
			using (FileStream fs = new FileStream(fileName, FileMode.Create))
			{
				formatter.Serialize(fs, serializableObject);
			}
		}

		/// <summary>
		/// Serializes the obj into XML file.
		/// </summary>
		/// <param name="serializableObject">The object to be serialized.</param>
		/// <param name="fileName">The file name_.</param>
		public static void ObjIntoXMLFile(this object serializableObject, string fileName)
		{
			if (ReferenceEquals(null,  serializableObject)) new ArgumentNullException("Null object given!");
			XmlSerializer formatter = new XmlSerializer(serializableObject.GetType());
			using (StreamWriter outfile = new StreamWriter(fileName))
			{
				formatter.Serialize(outfile, serializableObject);
			}
		}

		/// <summary>
		/// Serializes the obj into XML string.
		/// </summary>
		/// <param name="serializableObject">The object to be serialized.</param>
		/// <param name="fileName_">The file name_.</param>
		public static string ObjIntoXmlString(this object serializableObject)
		{
			if (ReferenceEquals(null,  serializableObject)) new ArgumentNullException("Null object given!");
			XmlSerializer formatter = new XmlSerializer(serializableObject.GetType());
			StringBuilder builder = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Encoding = Encoding.Default;
			settings.Indent = false;
			settings.OmitXmlDeclaration = true;
			settings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
			using (XmlWriter writer = XmlWriter.Create(builder, settings))
			{
				formatter.Serialize(writer, serializableObject);
			}
			string output = builder.ToString();
			return output;
		}

		/// <summary>
		/// Serializes the obj into binary file.
		/// </summary>
		/// <param name="serializableObject">The object to be serialized.</param>
		/// <param name="fileName">File name of the file to serialize to.</param>
		public static void ObjIntoBinFile(this object serializableObject, string fileName)
		{
			using (FileStream outfile = new FileStream(fileName, FileMode.Create, FileAccess.Write))
			{
				ObjIntoBinStream(serializableObject, outfile);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="serializableObject">The object to be serialized.</param>
		/// <param name="output">Stream to serialize to</param>
		public static void ObjIntoBinStream(this object serializableObject, Stream output)
		{
			if (ReferenceEquals(null, serializableObject)) new ArgumentNullException("Null object given!");
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(output, serializableObject);
		}

		/// <summary>
		/// Deserializes an new obj instance from XML file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="type">The type of the class that will be deserialized.</param>
		/// <returns>object if successfull</returns>
		public static object ObjFromXMLFile(string fileName, Type type)
		{
			using (StreamReader inFile = new StreamReader(fileName))
			{
				XmlSerializer formatter = new XmlSerializer(type);
				return formatter.Deserialize(inFile);
			}
		}

		public static object ObjFromXmlString(string xmlString, Type type)
		{
			using (StringReader input = new StringReader(xmlString))
			{
				XmlSerializer formatter = new XmlSerializer(type);
				return formatter.Deserialize(input);
			}
		}

		/// <summary>
		/// Deserializes an new obj instance from a binary file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <returns>object if successfull</returns>
		public static object ObjFromBinFile(string fileName)
		{
			using (FileStream inFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				return ObjFromBinStream(inFile);
			}
		}

		/// <summary>
		/// Deserializes an new obj instance from a binary stream.
		/// </summary>
		/// <param name="binData">The stream</param>
		/// <returns>object if successfull</returns>
		public static object ObjFromBinStream(Stream binData)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			return formatter.Deserialize(binData);
		}
	}
}
