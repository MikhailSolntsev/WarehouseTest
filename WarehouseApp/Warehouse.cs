using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data;

namespace WarehouseApp
{
    public class Warehouse
    {
        private readonly string boxFileName;

        private readonly string palletFileName;

        private readonly string containerFileName;

        private readonly Dictionary<int, Box> boxes = new();
        private readonly Dictionary<int, Pallet> pallets = new();

        public IReadOnlyDictionary<int, Box> Boxes { get => boxes; }
        public IReadOnlyDictionary<int, Pallet> Pallets { get => pallets; }

        public Warehouse()
        {
            boxFileName = Path.Combine(Environment.CurrentDirectory, "boxes.json");
            palletFileName = Path.Combine(Environment.CurrentDirectory, "pallets.json");
            containerFileName = Path.Combine(Environment.CurrentDirectory, "containers.json");
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

            var palletsOnly = pallets.Values.ToList();
            if (!string.IsNullOrEmpty(boxFileName) && palletsOnly.Count > 0)
            {
                storage = new(palletFileName);
                storage.WriteValues(palletsOnly);
            }

            if (!string.IsNullOrEmpty(containerFileName) && palletsOnly.Count > 0)
            {
                List<PalletBoxContainer> containers = Containers(palletsOnly);

                storage = new(containerFileName);
                storage.WriteValues(containers);
            }
        }

        public List<PalletBoxContainer> Containers(List<Pallet> palletsOnly)
        {
            return palletsOnly
                    .Select(pallet => (pallet.Id, pallet.Boxes))
                    .Select(pair => pair.Boxes.Select(box => new PalletBoxContainer(pair.Id, box.Id)))
                    .SelectMany(list => list)
                    .ToList();
        }

        public void ReadFromFiles()
        {
            JsonFileStorage storage;
            if (!string.IsNullOrEmpty(boxFileName) && File.Exists(boxFileName))
            {
                storage = new(boxFileName);
                var values = storage.ReadValues<Box>();
                values.ForEach(box => boxes.Add(box.Id, box));
            }

            if (!string.IsNullOrEmpty(palletFileName) && File.Exists(palletFileName))
            {
                storage = new(palletFileName);
                var values = storage.ReadValues<Pallet>();
                values.ForEach(pallet => pallets.Add(pallet.Id, pallet));
            }
            if (!string.IsNullOrEmpty(containerFileName) && File.Exists(containerFileName))
            {
                storage = new(containerFileName);
                var values = storage.ReadValues<PalletBoxContainer>();
                values.ForEach(container => pallets[container.PalletId].AddBox(boxes[container.BoxId]));
            }
        }

        public Box AddBox(Box box)
        {
            if (!boxes.ContainsKey(box.Id))
            {
                boxes.Add(box.Id, box);
            }
            return box;
        }
        public Pallet AddPallet(Pallet pallet)
        {
            if (!pallets.ContainsKey(pallet.Id))
            {
                pallets.Add(pallet.Id, pallet);
            }
            return pallet;
        }
        public Box AddBoxToPallet(Pallet pallet, Box box)
        {
            if (!pallets.ContainsKey(pallet.Id))
            {
                pallets.Add(pallet.Id, pallet);
            }
            if (!pallet.Boxes.Contains(box))
            {
                pallet.AddBox(box);
            }
            if (!boxes.ContainsKey(box.Id))
            {
                boxes.Add(box.Id, box);
            }
            return box;
        }

    }
}
