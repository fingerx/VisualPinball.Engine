// Visual Pinball Engine
// Copyright (C) 2020 freezy and VPE Team
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.IO;
using MessagePack;

namespace VisualPinball.Engine.Math
{
	[Serializable]
	[MessagePackObject]
	public class Vertex2D
	{
		[Key(0)]
		public float X;
		[Key(1)]
		public float Y;

		public float GetX() => X;
		public float GetY() => Y;

		public Vertex2D() : this(0.0f, 0.0f) { }

		public Vertex2D(float x, float y)
		{
			X = x;
			Y = y;
		}

		public Vertex2D(BinaryReader reader, int len)
		{
			X = reader.ReadSingle();
			Y = reader.ReadSingle();
			if (len > 8) {
				reader.BaseStream.Seek(len - 8, SeekOrigin.Current);
			}
		}

		public Vertex2D Set(float x, float y)
		{
			X = x;
			Y = y;
			return this;
		}

		public Vertex2D SetZero()
		{
			return Set(0, 0);
		}

		public Vertex2D Clone()
		{
			return new Vertex2D(X, Y);
		}

		public Vertex2D Add(Vertex2D v)
		{
			X += v.X;
			Y += v.Y;
			return this;
		}

		public Vertex2D Sub(Vertex2D v)
		{
			X -= v.X;
			Y -= v.Y;
			return this;
		}

		public Vertex2D Normalize()
		{
			var len = Length();
			return DivideScalar(len == 0 ? 1 : len);
		}

		public Vertex2D DivideScalar(float scalar)
		{
			return MultiplyScalar(1 / scalar);
		}

		public Vertex2D MultiplyScalar(float scalar)
		{
			X *= scalar;
			Y *= scalar;
			return this;
		}

		public float Length()
		{
			return MathF.Sqrt(X * X + Y * Y);
		}

		public float LengthSq()
		{
			return X * X + Y * Y;
		}

		public float Dot(Vertex2D pv)
		{
			return X * pv.X + Y * pv.Y;
		}

		public bool Equals(Vertex2D v)
		{
			if (v == null) {
				return false;
			}
			return X == v.X && Y == v.Y;
		}

		public override string ToString()
		{
			return $"Vertex2D({X}/{Y})";
		}
	}
}
