namespace AnotherDecoratorPatternExample
{

    // The base component interface defines operations that can be altered by decorators.

    public abstract class Component
    {
        public abstract string Operation();
    }

    // Concrete components provide default implementations of the operations.
    // There might be several variations of these classes.

    class ConcreteComponent : Component
    {
        public override string Operation()
        {
            return "ConcreteComponent";
        }
    }

    // The base decorator class follows the same interface as the other components.
    // The primary purpose of this class is to define the wrapping interface
    // for all concrete decorators. The default implementation of the wrapping
    // code might include a field for storing a wrapped component and the
    // means to initialize it.

    abstract class Decorator : Component
    {
        protected Component _component;
        public Decorator(Component component)
        {
            this._component = component;
        }

        public void SetComponent(Component component)
        {
            this._component = component;
        }

        // The decorator delegates all work to the wrapped component.
        public override string Operation()
        {
            if (this._component != null)
            {
                return this._component.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    // Concrete Decorators call the wrapped object and alter its result in some way.

    class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(Component component) : base(component)
        {
        }

        // Decorators may call parent implementation of the operation, instead
        // of calling the wrapped object directly. This approach simplifies
        // extension of decorator classes.
        public override string Operation()
        {
            return $"ConcreteDecoratorA({base.Operation()})";
        }
    }

    public class Client
    {
        // This client code works with all objects using the component interface.
        // This way it can stay independent of the concrete classes of components
        // it works with

        public void ClientCode(Component component)
        {
            System.Console.WriteLine("Result: " + component.Operation());
        }
    }



}