using DMS.OpenGL;
using System.IO;
using System.Text;

namespace DMS.ShaderDebugging
{
	public class ShaderFileDebugger
	{
		//public delegate void ShaderLoadedHandler();
		//public event ShaderLoadedHandler ShaderLoaded;

		public ShaderFileDebugger(string vertexFile, string fragmentFile,
			byte[] vertexShader = null, byte[] fragmentShader = null)
		{
			if (File.Exists(vertexFile) && File.Exists(fragmentFile))
			{
				shaderWatcherVertex = new FileWatcher(vertexFile);
				shaderWatcherVertex.Changed += (s, e) => form.Close();
				shaderWatcherFragment = new FileWatcher(fragmentFile);
				shaderWatcherFragment.Changed += (s, e) => form.Close();
			}
			else
			{
				var sVertex = ReferenceEquals(null, vertexShader) ? string.Empty : Encoding.UTF8.GetString(vertexShader);
				var sFragment = ReferenceEquals(null, fragmentShader) ? string.Empty : Encoding.UTF8.GetString(fragmentShader);
				shader = ShaderLoader.FromStrings(sVertex, sFragment);
				//ShaderLoaded?.Invoke(); //is null because we are in the constructor
			}
		}

		public bool CheckForShaderChange()
		{
			//test if we even have file -> no files nothing to be done
			if (ReferenceEquals(null, shaderWatcherVertex) || ReferenceEquals(null, shaderWatcherFragment)) return false;
			//test if any file is dirty
			if (!shaderWatcherVertex.Dirty && !shaderWatcherFragment.Dirty) return false;
			try
			{
				shader = ShaderLoader.FromFiles(shaderWatcherVertex.FullPath, shaderWatcherFragment.FullPath);
				shaderWatcherVertex.Dirty = false;
				shaderWatcherFragment.Dirty = false;
				//ShaderLoaded?.Invoke();
				return true;
			}
			catch (IOException e)
			{
				var exception = new ShaderException(e.Message, string.Empty);
				ShowDebugDialog(exception);
			}
			catch (ShaderException e)
			{
				ShowDebugDialog(e);
			}
			return false;
		}

		private void ShowDebugDialog(ShaderException exception)
		{
			var newShaderCode = form.ShowModal(exception);
		}

		public Shader Shader { get { return shader; } }

		private Shader shader;
		private FileWatcher shaderWatcherVertex = null;
		private FileWatcher shaderWatcherFragment = null;
		private readonly FormShaderExceptionFacade form = new FormShaderExceptionFacade();
	}
}
