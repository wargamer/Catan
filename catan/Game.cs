using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace catan
{

    class Base { }

    class BA : Base { }
    class BB : Base { }
    class Game
    {
        List<Structure> Structures = new List<Structure>();
        CatanBoard Board = new CatanBoard();

        public Game()
        {
            var bases = new List<Base> { (Base)new BA(), (Base)new BB() };

            foreach (var b in bases)
                if (b is BB)
                    ;
            var bbs = bases.OfType<BB>();



            Board.Tiles.Add(new Tile(TileType.Water, 0));
            List<Tile> allTiles = new List<Tile>();
            Random rand = new Random();
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
                    return;
        }
    }
}
