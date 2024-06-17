using System;
using UnityEngine;

namespace FpvDroneSimulator.Common.Math.Types
{
    [Serializable]
    public struct Coords
    {
        [field:SerializeField] public int X { get; private set; }
        [field:SerializeField] public int Y { get; private set; }

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator (int, int)(Coords coords) => (coords.X, coords.Y);
        public static implicit operator Coords((int x, int y) coords) => new Coords(coords.x, coords.y);

        public static bool operator ==(Coords @this, Coords that) => 
            @this.X == that.X && @this.Y == that.Y;

        public static bool operator !=(Coords @this, Coords that) => 
            !(@this == that);

        public static Coords operator +(Coords @this, Coords that) =>
            new Coords(@this.X + that.X, @this.Y + that.Y);
        
        public static Coords operator -(Coords @this, Coords that) =>
            new Coords(@this.X - that.X, @this.Y - that.Y);
        
        public static Coords operator /(Coords @this, int divider) =>
            new Coords(@this.X / divider,@this.Y / divider);
        public static Coords operator *(Coords @this, int multiplier) =>
            new Coords(@this.X * multiplier,@this.Y * multiplier);

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public override string ToString() => $"({X}, {Y})";
        public bool Equals(Coords other) => X == other.X && Y == other.Y;

        public override bool Equals(object? obj) => obj is Coords other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}