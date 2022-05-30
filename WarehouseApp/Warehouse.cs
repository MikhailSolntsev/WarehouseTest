using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    public class Warehouse
    {
        private readonly string boxFileName;

        private readonly string palletFileName;

        private readonly string containerFileName;

        private readonly Dictionary<string, Box> boxes = new();
        private readonly Dictionary<string, Pallet> palletes = new();

        public IReadOnlyDictionary<string, Box> Boxes { get => boxes; }
        public IReadOnlyDictionary<string, Pallet> Palletes { get => palletes; }

        public Warehouse()
        {
            boxFileName = System.IO.Path.Combine(Environment.CurrentDirectory, "boxes.json");
            palletFileName = System.IO.Path.Combine(Environment.CurrentDirectory, "palletes.json");
            containerFileName = System.IO.Path.Combine(Environment.CurrentDirectory, "containers.json");
        }

        public Warehouse(string boxFileName, string palletFileName, string containerFileName)
        {
            this.boxFileName = boxFileName;
            this.palletFileName = palletFileName;
            this.containerFileName = containerFileName;
        }

        public void SaveToFiles()
        {
            JsonFileStorage storage;

            if (!string.IsNullOrEmpty(boxFileName) && boxes.Count > 0)
            {
                storage = new(boxFileName);
                storage.WriteValues(boxes.Values.ToList());
            }

            var palletesOnly = palletes.Values.ToList();
            if (!string.IsNullOrEmpty(boxFileName) && palletesOnly.Count > 0)
            {
                storage = new(palletFileName);
                storage.WriteValues(palletesOnly);
            }

            if (!string.IsNullOrEmpty(containerFileName) && palletesOnly.Count > 0)
            {
                var ids = palletesOnly
                    .Select(pallet => (pallet.PalletId, pallet.Boxes))
                    .Select(pair => pair.Boxes.Select(box => (pair.PalletId, box.BoxId)))
                    .ToList();

                storage = new(containerFileName);
                storage.WriteValues(ids);
            }
        }

        public void ReadFromFiles()
        {
            JsonFileStorage storage;
            if (!string.IsNullOrEmpty(boxFileName))
            {
                storage = new(boxFileName);
                storage.ReadValues<Box>().ForEach(box => boxes.Add(box.BoxId, box));
            }

            if (!string.IsNullOrEmpty(palletFileName))
            {
                storage = new(palletFileName);
                storage.ReadValues<Pallet>().ForEach(pallet => palletes.Add(pallet.PalletId, pallet));
            }
            if (!string.IsNullOrEmpty(containerFileName))
            {
                storage = new(containerFileName);
                storage.ReadValues<(string, string)>().ForEach(pair => palletes[pair.Item1].AddBox(boxes[pair.Item2]));
            }
        }

        public Box AddBox(Box box)
        {
            if (!boxes.ContainsKey(box.BoxId))
            {
                boxes.Add(box.BoxId, box);
            }
            return box;
        }
        public Pallet AddPallet(Pallet pallet)
        {
            if (!palletes.ContainsKey(pallet.PalletId))
            {
                palletes.Add(pallet.PalletId, pallet);
            }
            return pallet;
        }
        public Box AddBoxToPallet(Pallet pallet, Box box)
        {
            if (!palletes.ContainsKey(pallet.PalletId))
            {
                palletes.Add(pallet.PalletId, pallet);
            }
            if (!pallet.Boxes.Contains(box))
            {
                pallet.AddBox(box);
            }
            if (!boxes.ContainsKey(box.BoxId))
            {
                boxes.Add(box.BoxId, box);
            }
            return box;
        }
    }
}
