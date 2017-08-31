using DMS.OpenGL;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DMS.ShaderDebugging
{
	public class FormShaderExceptionFacade
	{
		public string ShowModal(ShaderException e)
		{
			form = new FormShaderException();
			form.Text = e.Message;
			var compileException = e as ShaderCompileException;
			if (ReferenceEquals(null, compileException))
			{
				form.ShaderSourceCode = string.Empty;
			}
			else
			{
				form.ShaderSourceCode = compileException.ShaderCode;
			}
			//load error list after source code is loaded for highlighting of error to work
			form.Errors.Clear();
			var log = new ShaderLog(e.ShaderLog);
			foreach (var logLine in log.Lines)
			{
				form.Errors.Add(logLine);

			}
			var shaderFileName = e.ExtractFileName();
			if (string.IsNullOrEmpty(shaderFileName))
			{
				foreach (var logLine in log.Lines)
				{
					Debug.Print(shaderFileName + "(" + logLine.LineNumber + "): " + logLine.Message);
				}
			}
			form.Select(0);
			form.TopMost = true;
			var oldSource = form.ShaderSourceCode;
			closeOnFileChange = true;
			var result = form.ShowDialog();
			closeOnFileChange = false;
			var newShaderSourceCode = DialogResult.OK == result ? form.ShaderSourceCode : oldSource;
			form = null;

			if (ReferenceEquals(null, compileException)) return newShaderSourceCode;
			if (newShaderSourceCode != compileException.ShaderCode && !string.IsNullOrEmpty(shaderFileName))
			{
				//save changed code to shaderfile
				File.WriteAllText(shaderFileName, newShaderSourceCode);
			}
			return newShaderSourceCode;
		}

		public void Close()
		{
			if (ReferenceEquals(null, form)) return;
			if (!closeOnFileChange) return;
			form.Invoke((MethodInvoker)delegate
			{
				form.Close();
			});
		}

		private FormShaderException form = null;
		private bool closeOnFileChange = false;
	}
}
