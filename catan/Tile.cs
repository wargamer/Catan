using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace catan
{
    [DebuggerDisplay("{TileType}, {Number}")]
    class Tile
    {
        public Tile(TileType type, int number)
        {
            Number = number;
            TileType = type;
            id = newId;
        }

        public Tile(TileType type)
        {
            TileType = type;
            id = newId;
        }

        public void addNeighbor(Tile t, NeighborLocation loc)
        {
            if (!Neighbors.ContainsKey(loc))
                Neighbors.Add(loc, t);
        }

        public Boolean hasNeighbor(NeighborLocation loc)
        {
            return Neighbors.ContainsKey(loc);
        }

        public int Number;
        public readonly TileType TileType;
        public readonly int id;
        public readonly Dictionary<NeighborLocation, Tile> Neighbors = new Dictionary<NeighborLocation, Tile>();
        private static int _idCounter = 0;
        private static int newId { get { return _idCounter++;}}
    }

    enum NeighborLocation
    {
        Top = 0,
        TopRight =1,
        BottomRight =2,
        Bottom =3,
        BottomLeft =4,
        TopLeft =5,
    }
}
