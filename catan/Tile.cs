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

        public void addNeighbor(Tile t)
        {
            Neighbors.Add(t);
        }

        public int Number;
        public TileType TileType;
        public readonly int id;
        public readonly List<Tile> Neighbors = new List<Tile>();
        private static int _idCounter = 0;
        private static int newId { get { return _idCounter++;}}
    }
}
