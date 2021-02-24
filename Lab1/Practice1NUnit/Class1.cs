using System;

public class Class1
{
	int value;

	public Class1(int value)
	{
		this.value = value;
	}
	public double formula(int value)
	{
		return (sqrt(value) - sqrt(1 - value));
	}
}
