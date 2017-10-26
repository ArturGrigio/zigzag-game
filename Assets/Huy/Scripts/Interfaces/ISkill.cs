using System;

namespace ZigZag
{
	public interface ISkill
	{
		bool IsActive { get; }
		bool IsEnabled { get; set; }

		void Activate();
		void Deactivate();
	}
}

