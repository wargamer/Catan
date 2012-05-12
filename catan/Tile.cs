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
            Neighbors.Add(loc, t);
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
        Top,
        TopRight,
        BottomRight,
        Bottom,
        BottomLeft,
        TopLeft,
    }
}
