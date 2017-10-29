using System;

namespace ZigZag
{
	public interface IMovement
	{
		void Move(float x, float y);
		void Jump(bool jumpFlag);
	}
}
