using System;
using System.Collections.Generic;

namespace Work43
{
    public class Animal : IComparable<Animal>
    {
        private string _genus { get; set; }
        private int _weight { get; set; }
        public string Genus
        {
            get { return _genus; }
        }
        public int Weight
        {
            get { return _weight; }
        }
        public Animal() { }
        public Animal(string genus, int weight)
        {
            this._genus = genus;
            this._weight = weight;
        }
        public override string ToString()
        {
            return _genus + "-" + _weight;
        }
        public int CompareTo(Animal animal)
        {
            return String.Compare(this._genus, animal.Genus);
        }
        public static void SortWeightAscending(Animal[] animal)
        {
            Array.Sort(animal, new SortWeightAscendingHelper());
        }
        public static void SortGenusDescending(Animal[] animal)
        {
            Array.Sort(animal, new SortGenusDescendingHelper());
        }

        internal class SortWeightAscendingHelper : IComparer<Animal>
        {
            public int Compare(Animal x, Animal y)
            {
                if (x.Weight > y.Weight)
                    return 1;
                if (x.Weight < y.Weight)
                    return -1;
                else return 0;
            }
        }
        internal class SortGenusDescendingHelper : IComparer<Animal>
        {
            public int Compare(Animal x, Animal y)
            {
                return String.Compare(y.Genus, x.Genus);
            }
        }
    }
}
