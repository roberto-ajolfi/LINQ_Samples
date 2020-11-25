using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ.ConsoleApp
{
    public class Parent
    {
        public int ID { get; set; }

        public virtual void MethodOne() { }
    }

    public class ParentTwo
    {
        public int ID2 { get; set; }
    }

    public interface IParentTwo
    {
        int ID2 { get; set; }
    }

    // EREDITA SOLO DA Parent, IMPLEMENTA IParentTwo (nessun legame di ereditarietà con ParentTwo)
    //
    // L'implementazione di IParentTwo NON HA legami con l'implementazione di ParentTwo (va riscritta)
    public class Child : Parent, IParentTwo
    {
        public int ID2 { get; set; }
    }

    // EREDITA SOLO DA Parent, IMPLEMENTA IParentTwo (nessun legame di ereditarietà con ParentTwo)
    //
    // COMPOSIZIONE : incapsula una istanza di ParentTwo, l'implementazione di IParentTwo si 
    //                appoggia all'istanza di ParentTwo
    public class ChildTwo : Parent, IParentTwo
    {
        private ParentTwo ParentTwo { get; set; }

        public int ID2 { get => ParentTwo.ID2; set => ParentTwo.ID2 = value; }
    }
}
