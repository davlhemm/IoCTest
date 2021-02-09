using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IoCTest.Interfaces
{
    public enum Animal
    {
        Person = 0,
        Dog = 1,
        Cat = 2,
        Horse = 3
    }

    public delegate IAnimal AnimalCreationDelegate();

    public interface IAnimal
    {
        int Legs { get; }
    }

    public class Cat : IAnimal
    {
        public int Legs { get; set; } = 4;
    }

    public class Person : IAnimal
    {
        public int Legs => 2;
    }

    public class Horse : IAnimal
    {
        public int Legs => 4;
    }

    public class Dog : IAnimal
    {
        public int Legs => 4;
    }

    public class AnimalDescriptor
    {
        public Animal Type;

        // This ends up being a delegate
        public AnimalCreationDelegate Creator;
    }

    public class AnimalFactory
    {
        private readonly IList<AnimalDescriptor> _creatorList;

        public AnimalFactory(IList<AnimalDescriptor> creators)
        {
            _creatorList = creators;
        }

        public IAnimal Create(Animal type)
        {
            AnimalCreationDelegate creator = _creatorList.FirstOrDefault(x => x.Type == type)?.Creator;
            if (creator != null) return creator();
            else
            {
                throw new NullReferenceException($"Can't create delegate due to lack of creator type in Create(...) for {type}");
            }
        }
    }
}