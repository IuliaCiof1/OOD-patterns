using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    /* Factory Method pattern != Simple Factory idiom
     * - creational design pattern
     * - Defines a method which is used for creating objects instead of using direct constructor calls. 
     * Subclases can override this method to change the class of objects that will be created.
     * - respects Single Responsibility and Open/Closed principles
     * - provides an interface for creating objects in a superclass
     * - allows subclasses to decide which type to instantiate
     * - used when you don't know which type of object will be initiated
     * - loosens the coupling of given code by separating the product's construction code from the code that uses this product
     *  - provides a high level of flexibility for your code
     */

    
    // **** Product Interface ****
    // Declares the operations that concrete products must implement
    public interface Burger
    {

       void prepare();
    }

    //  **** Concrete Products  ****
    // They provide various implementations for the Burger (/Product) interface
    class BeefBurger:Burger 
    {
        bool angus;

        
        public void prepare()
        {
            Console.WriteLine("....preparing beef burger.....");
        }

       
    }

    class VeggieBurger:Burger
    {
        bool combo;

        public void prepare()
        {
            Console.WriteLine("....preparing veggie burger.....");
        }
    }

    //class SimpleBurgerFactory  //factory = class whose sole responsability is creating burgers; only here the concrete type of burgers is known
    //{

    //    //not a design pattern - it is still open for modification (when adding new recipes we will have to change the method and add if statements
    //    public Burger createBurger(string request)
    //    {
    //        Burger burger = null;
    //        if ("BEEF".Equals(request))
    //        {
    //            burger = new BeefBurger();
    //        }
    //        else if ("VEGGIE".Equals(request))
    //        {
    //            burger = new VeggieBurger();
    //        }

    //        return burger;
    //    }
    //}

    // **** Creator ****
    // Creator class declares the factory method that will return an object of a Product class
    // Its subclasses usually provide the implementation of the factory method
    // This class may provide some default implementation of the f method
    // Creator class DOES NOT create products!

    abstract class Restaurant
    {
        public Burger orderBurger()
        {
            Burger burger = createBurger();

            burger.prepare();
            return burger;
        }

        public abstract Burger createBurger(); //The Factory Method
    }

    // **** Concrete Creators ****
    //Their job is to override the factory method in order to change the resulting product's type
    //They are independent of concrete product classes
    class BeefBurgerRestaurant : Restaurant
    {
        public override Burger createBurger()
        {
            return new BeefBurger();
        }
    }

    class VeggieBurgerRestaurant:Restaurant
    {
        public override Burger createBurger()
        {
            return new VeggieBurger();
        }
    }


    // **** Client code ****
    class Program_FactoryMethod
    {
        static void Main(string[] args)
        {

            Restaurant beefResto = new BeefBurgerRestaurant();
            Burger beefBurger = beefResto.orderBurger();

            Restaurant veggieResto = new VeggieBurgerRestaurant();
            Burger veggieBurger = veggieResto.orderBurger();

            Console.ReadKey();
        }
    }
}
