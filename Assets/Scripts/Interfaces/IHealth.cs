using System;

public interface IHealth
{
	void SetHealth(float scaledDamage);
	void ReceiveDamage(float damage);
}

