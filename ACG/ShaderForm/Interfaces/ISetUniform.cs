namespace ShaderForm.Interfaces
{
	public interface ISetUniform
	{
		void SetUniform(string uniformName, float value);
		void SetUniform(string uniformName, float valueX, float valueY);
		void SetUniform(string uniformName, float valueX, float valueY, float valueZ);
	}
}