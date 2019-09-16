using System;
using System.Collections;
using System.Collections.Generic;

namespace Work43
{
    class Animals : IEnumerable
    {
        private Animal[] animals;
        public Animals()
        {
            animals = new Animal[] 
            {
                new Animal("elephant", 5000),
                new Animal("lion", 800),
                new Animal("buffalo", 1200),
                new Animal("antelope", 75),
            };
        }
        public IEnumerator GetEnumerator()
        {
            return new AnimalsEnumerator(animals);
        }
    }

    internal class AnimalsEnumerator : IEnumerator<Animal>
    {
        private Animal[] animals;
        private int position = -1;
        public AnimalsEnumerator(Animal[] animals)
        {
            this.animals = animals;
        }

        public Animal Current
        {
            get
            {
                if (position == -1 || position >= animals.Length)
                    throw new InvalidOperationException();
                return animals[position];
            }
        }
        object IEnumerator.Current => animals[position];
        public bool MoveNext()
        {
            if (position < animals.Length - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }
        public void Reset()
        {
            position = -1;
        }
        public void Dispose() { }
    }
}
