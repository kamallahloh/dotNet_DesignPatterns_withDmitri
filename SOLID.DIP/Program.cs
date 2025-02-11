using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
namespace SOLID.DIP
{

    //namespace DotNetDesignPatternDemos.SOLID.DependencyInversionPrinciple
    // your store                           other 3rd party modules
    // high level modules should not depend on low-level; both should depend on abstractions // PATTERNS facade or adapter
    // abstractions should not depend on details; details should depend on abstractions

    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
        // public DateTime DateOfBirth;
    }

    public interface IRelationshipBrowser // help to contact the High level
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // low-level
    public class Relationships : IRelationshipBrowser // low-level
    {
        private List<(Person, Relationship, Person)> relations // with Dependency inversion you will keep this private property
          = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public List<(Person, Relationship, Person)> Relations => relations; // without Dependency inversion need to extract the values to public property

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations
              .Where(x => x.Item1.Name == name
                          && x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }
    }

    public class Research
    {
        //public Research(Relationships relationships) // high-level have access to low-level modules // danger exposes you private property of relationships 
        //{
        //    //high - level: find all of john's children
        //    //var relations = relationships.Relations;
        //    //foreach (var r in relations
        //    //  .Where(x => x.Item1.Name == "John"
        //    //              && x.Item2 == Relationship.Parent))
        //    //{
        //    //    WriteLine($"John has a child called {r.Item3.Name}"); // System.ValueTuple to get items 1 2 3 <(Person, Relationship, Person)>
        //    //}
        //}

        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                WriteLine($"John has a child called {p.Name}");
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Matt" };

            // low-level module
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}
