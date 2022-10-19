﻿using System;

using NUnit.Framework;

namespace Force.DeepCloner.Tests
{
	[TestFixture(true)]
	public class ConstructorsSpec : BaseTest
	{
		public ConstructorsSpec(bool isSafeInit)
			: base(isSafeInit)
		{
		}

		public class T1
		{
			private T1()
			{
			}

			public static T1 Create()
			{
				return new T1();
			}

			public int X { get; set; }
		}

		public class T2
		{
			public T2(int arg1, int arg2)
			{
			}

			public int X { get; set; }
		}

		public class ExClass
		{
			public ExClass()
			{
				throw new Exception();
			}

			public ExClass(string x)
			{
				// does not throw here
			}

			public override bool Equals(object obj)
			{
				throw new Exception();
			}

			public override int GetHashCode()
			{
				throw new Exception();
			}

			public override string ToString()
			{
				throw new Exception();
			}
		}
		
		[Test]
		public void Object_With_Private_Constructor_Should_Be_Cloned()
		{
			var t1 = T1.Create();
			t1.X = 42;
			var cloned = t1.DeepClone();
			t1.X = 0;
			Assert.That(cloned.X, Is.EqualTo(42));
		}

		[Test]
		public void Object_With_Complex_Constructor_Should_Be_Cloned()
		{
			var t2 = new T2(1, 2);
			t2.X = 42;
			var cloned = t2.DeepClone();
			t2.X = 0;
			Assert.That(cloned.X, Is.EqualTo(42));
		}

		[Test]
		public void Anonymous_Object_Should_Be_Cloned()
		{
			var t2 = new { A = 1, B = "x" };
			var cloned = t2.DeepClone();
			Assert.That(cloned.A, Is.EqualTo(1));
			Assert.That(cloned.B, Is.EqualTo("x"));
		}


		[Test]
		public void Cloner_Should_Not_Call_Any_Method_Of_Class_Be_Cloned()
		{
			Assert.DoesNotThrow(() => new ExClass("x").DeepClone());
			var exClass = new ExClass("x");
			Assert.DoesNotThrow(() => new[] { exClass, exClass }.DeepClone());
		}
	}
}
