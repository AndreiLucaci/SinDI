# SinDI
Simple DI container 

## Usage

1. Instance registration

```cs
ISinContainer container = new SinContainer();
string elem = "something nice";
container.Register(elem);
var resolvedString = container.Resolve<string>();
```

2. Type registration - concrete

```cs
ISinContainer container = new SinContainer();
container.Register<ConcreteObject>();
var resolvedConcreteObject = container.Resolve<ConcreteObject>();
```

3. Type registration - abstract / interface

```cs
ISinContainer container = new SinContainer();
container.Register<ISomeInterface, ConcreteObject>();
var resolvedConcreteObject = container.Resolve<ISomeInterface>();
```

4. Registration with known constructor - already instantiated object

```cs
ISinContainer container = new SinContainer();
var someInstanciatedObject = new SomeInstanciatedObject(); // dummy objects
container.Register<ConcreteObject>(new KnownCtor(someInstanciatedObject));
var resolvedConcreteObject = container.Resolve<ConcreteObject>();
```

5. Registration with known constructor - already registered object type

```cs
class ConcreteObject
{
    public ConcreteObject(SomeObject someObject)
    {
        // implementation
    }
}

// ...

ISinContainer container = new SinContainer();
container.Register<SomeObject>();
container.Register<ConcreteObject>();
var resolvedConcreteObject = container.Resolve<ConcreteObject>();
```

### 6. Registration with known constructor - already registered object type via interface

```cs
class ConcreteObject
{
    public ConcreteObject(ISomeObject someObject)
    {
        // implementation
    }
}

// ...

ISinContainer container = new SinContainer();
container.Register<ISomeObject, SomeObject>();
container.Register<ConcreteObject>();
var resolvedConcreteObject = container.Resolve<ConcreteObject>();
```

### Exceptions
1. CtorNotFountException
```cs
public class CtorNotFoundException : Exception
{
    // implementation
}
```
Thrown when the constructor for the object to be resolved is not found

2. TypeNotFoundException
```cs
public class TypeNotFoundException : Exception
{
    // implementation
}
```
Thrown when the trying to resolve an unregistered type

### Info
Built with a hash algorithm for storing references and quickly accessing them.

### Missing parts
1. Registering objects via name

e.g.
```cs
ISinContainer container = new SinContainer();
container.Register<SomeObject>("someObject1");
container.Register<SomeObject>("someObject2");

var obj1 = container.Resolve<SomeObject>("someObject1");
var obj2 = container.Resolve<SomeObject>("someObject2");

Assert.NotEqual(obj1, obj2); // true
```

2. Extend with unit tests
3. Extract nuget package - ongoing
4. Add attribute for name specific resolving of objects (needs 1st point)

