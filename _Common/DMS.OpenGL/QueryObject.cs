using DMS.Base;
using OpenTK.Graphics.OpenGL;

namespace DMS.OpenGL
{
	public class QueryObject : Disposable
	{
		public QueryObject()
		{
			GL.GenQueries(1, out id);
		}

		public void Activate(QueryTarget target)
		{
			Target = target;
			GL.BeginQuery(target, id);
		}

		public void Deactivate()
		{
			GL.EndQuery(Target);
		}

		public bool IsFinished
		{
			get
			{
				int isFinished;
				GL.GetQueryObject(id, GetQueryObjectParam.QueryResultAvailable, out isFinished);
				return 1 == isFinished;
			}
		}

		public int Result
		{
			get
			{
				int result;
				GL.GetQueryObject(id, GetQueryObjectParam.QueryResult, out result);
				return result;
			}
		}

		public long ResultLong
		{
			get
			{
				long result;
				GL.GetQueryObject(id, GetQueryObjectParam.QueryResult, out result);
				return result;
			}
		}

		public QueryTarget Target { get; private set; }

		public bool TryGetResult(out int result)
		{
			result = -1;
			GL.GetQueryObject(id, GetQueryObjectParam.QueryResultNoWait, out result);
			return -1 != result;
		}

		public bool TryGetResult(out long result)
		{
			result = -1;
			GL.GetQueryObject(id, GetQueryObjectParam.QueryResultNoWait, out result);
			return -1 != result;
		}

		protected override void DisposeResources()
		{
			GL.DeleteQueries(1, ref id);
		}

		private int id;
	}
}
