using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace catan
{
    class Game
    {
        List<Structure> Structures = new List<Structure>();
        CatanBoard Board = new CatanBoard();

        public Game()
        {
            int NumberOfTiles = 19;
            var NumberHeap = new Heap<int>(NumberOfTiles) { 2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12 };
        //    var LandTileHeap = new Heap<Tile>(NumberOfTiles);
            LandTileHeap.Add(3, TileType.Ore);
            LandTileHeap.Add(3, TileType.Brick);
            LandTileHeap.Add(4, TileType.Grain);
            LandTileHeap.Add(4, TileType.Lumber);
            LandTileHeap.Add(4, TileType.Wool);
            LandTileHeap.Add(1, TileType.Desert);

            List<Tile> allTiles = new List<Tile>(LandTileHeap.AsEnumerable());
            Random rand = new Random();
            var centertile = LandTileHeap.TakeAway(rand.Next()%LandTileHeap.Count);

            for (int i = 0; i < 6; ++i)
            {
                centertile.addNeighbor(LandTileHeap.TakeAway(rand.Next() % LandTileHeap.Count), (NeighborLocation) i);
                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(centertile, (NeighborLocation)((i + 3) % 6));
            }

            for (int i = 0; i < 6; ++i)
            {
                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 1) % 6)).Value, (NeighborLocation)((i + 2) % 6));
                var z = (NeighborLocation)((i + 4) % 6);
                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 5) % 6)).Value, z);
            }

            for (int i = 0; i < 6; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(LandTileHeap.TakeAway(rand.Next() % LandTileHeap.Count), (NeighborLocation)((i + j) % 6));
                    centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)((i + j) % 6)).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)(i % 6)).Value, (NeighborLocation)((i + j + 3) % 6));
                }
            }

            for (int i = 0; i < 6; ++i)
            {
                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 5) % 6)).Value.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value, (NeighborLocation)((i + 5) % 6));
                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 5) % 6)).Value.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value, (NeighborLocation)((i + 2) % 6));

                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 1) % 6)).Value, (NeighborLocation)((i + 2) % 6));
                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 1) % 6)).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value, (NeighborLocation)((i + 5) % 6));

                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 5) % 6)).Value, (NeighborLocation)((i + 4) % 6));
                centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)((i + 5) % 6)).Value.addNeighbor(centertile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value, (NeighborLocation)((i + 1) % 6));
            }

            Debug.Assert(centertile.Neighbors.All(n => n.Value.Neighbors.Count == 3));
            foreach (var n in allTiles)
            {
                Debug.WriteLine("\nTile {0} has neighbors:", n.id);
                foreach (var n2 in n.Neighbors)
                    Debug.Write("\t" + n2.Key + "\t " +n2.Value.id + Environment.NewLine);
            }

            var tiletypes = Enum.GetValues(typeof(TileType));
            int awardNumber = 0;
            allTiles.Add(new Tile(TileType.Desert, 0));
            while (allTiles.Count < 20)
            {
                Tile newTile = new Tile((TileType)tiletypes.GetValue(rand.Next(tiletypes.Length)), awardNumber);

                while ((awardNumber = rand.Next(2, 12)) == 7)
                    ;
                allTiles.Add(newTile);
            }
            while(allTiles.Count < 38)
                allTiles.Add(new Tile(TileType.Water, 0));


            List<Structure> allStructures = new List<Structure>();
            int dicerollNumber = 1;

            var luckyTiles = allTiles.Where(t => t.Number == dicerollNumber);
            var luckyCombos = allStructures.Select(s =>
            {
                var luckytiletype = s.Location.Single(t => luckyTiles.Contains(t));
                return new { selectedStruct = s, selectedTileType = luckytiletype.TileType };
            });

            foreach (var combo in luckyCombos)
                if (combo.selectedTileType == TileType.Water)
                    ;//return;
        }

        private void checkNeighbors(Tile currentTile, int widthCounter)
        {
            widthCounter++;
            for (int i = 0; i < 6; ++i)
            {
                int a = 0;
                a++;
                Debug.Assert(LandTileHeap.Count != 0);

                if (!currentTile.hasNeighbor((NeighborLocation)i))
                {
                    currentTile.addNeighbor(LandTileHeap.TakeAway(rand.Next() % LandTileHeap.Count), (NeighborLocation)i);
                    currentTile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value.addNeighbor(currentTile, (NeighborLocation)((i + 3) % 6));
                    Tile tempTile = currentTile.Neighbors.Single(n => n.Key == (NeighborLocation)i).Value;

                    for (int j = 0; j < i; ++j)
                        tempTile.addNeighbor(currentTile.Neighbors.Single(n => n.Key == (NeighborLocation)j).Value, (NeighborLocation)(((j + 3) % 6) - 1));


                    if (widthCounter < 3)
                        checkNeighbors(tempTile, widthCounter);
                }
            }
        }

        public void BuildBoard()
        {
            LandTileHeap.Add(3, TileType.Ore);
            LandTileHeap.Add(3, TileType.Brick);
            LandTileHeap.Add(4, TileType.Grain);
            LandTileHeap.Add(4, TileType.Lumber);
            LandTileHeap.Add(4, TileType.Wool);
            LandTileHeap.Add(1, TileType.Desert);
            Tile centertile = new Tile(TileType.Desert, 0);

            checkNeighbors(centertile, 0);
        }

        public Heap<Tile> LandTileHeap = new Heap<Tile>(19);
        public Random rand = new Random();
    }
}
