# The `Maybe` monad in C# #

I've often wanted to use the F# `option` type in C#. So I wrote the `Maybe` class which works like it.

## Discriminated Union ##

`Maybe<T>` is a discriminated union type with two possible value constructors: `Some` and `None`.

The presence of a value of type `T` is signified by lifting it into a `Maybe<T>` using the `Some` constructor.
Conversely, the absence of a value is signified by the use of the `None` constructor.

The discriminated union is implemented using an abstract class with a private, closed set of heirs.

Since the set of heirs is both private and closed, there is no way of directly deciding what the type of a given instance of `Maybe<T>` is.
This necessitates a matching mechanism which invokes a handler based on the type of the instance.

Note that this means that there is **no type comparison**, nor is there any `if-then-else` checking.

## Examples ##

* Creating a `Maybe` to signify the absence of any value

``` csharp
var none = Maybe<int>.None;
// none : Maybe<int> [None]
```

* Creating a `Maybe` to signify the presence of a value of type `string`

``` csharp
var key = Maybe<string>.Some("The key to the kingdom");
// key : Maybe<string> [Some("The key to the kingdom")]
```

* Given an instance of `Maybe<int>`, add 1 to the value if it is present and return the incremented value, or 0 if no value is present

``` csharp
// arg : Maybe<int> [??]
var result = arg.Match(v => v + 1, () => 0);
// result : Maybe<int> [(n + 1) -or- 0]
```

* Given an instance of `Maybe<string>`, print the value if it is present or the string "Missing"

``` csharp
// arg : Maybe<string> [??]
arg.Iter(Console.WriteLine, () => Console.WriteLine("Missing"));
```
## Value Semantics ##
Instances of the `Maybe` class exhibit value semantics. This means that two instances of `Some` with the same value will report equality when compared, _even if their references are not_. This is done by implementing the `IEquatable<Maybe<T>>` and `IStructuralEquatable` interfaces, and providing custom `==` and `!=` operators.

The following examples all print `True`:

``` csharp
Console.WriteLine($"None   == None   : {Maybe<int>.None == Maybe<int>.None}");
Console.WriteLine($"Some 5 == Some 5 : {Maybe<int>.Some(5) == Maybe<int>.Some(5)}");
Console.WriteLine($"Some 6 != Some 5 : {Maybe<int>.Some(6) != Maybe<int>.Some(5)}");
Console.WriteLine($"Some 2 != None   : {Maybe<int>.Some(2) != Maybe<int>.None}");
Console.WriteLine($"None   != Some 2 : {Maybe<int>.None != Maybe<int>.Some(2)}");
```

## Functor ##

The `Maybe` class can implement a `map` function, essentially making it a Functor.

### `Map` ###

* Given an instance of `Maybe<int>`, increment any present value and return it

``` csharp
// arg : Maybe<int> [??]
var result = arg.Map(v => v + 1);
// result : Maybe<int> [Some (n + 1) -or- None]
```

## Foldable ##

The `Maybe` class can be structurally folded and its value liberated.

### `GetOrElse` ###

* Given an instance of `Maybe<string>`, extract any present string value, otherwise return the empty string.

``` csharp
// arg : Maybe<string> [??]
var result = arg.GetOrElse(x => x, String.Empty);
// result : string [value -or- String.Empty]
```

* Given an instance of `Maybe<int>`, extract the string representation of any value present, otherwise an empty string.

``` csharp
// arg : Maybe<int> [??]
var result = arg.GetOrElse(x => x.ToString(), String.Empty);
// result : string [value -or- String.Empty]
```

### `Fold` ###

`Fold` with traditional arguments provided for sake of completeness.

## Monad ##

The `Maybe` class can be treated as a monad, and computations on the value can be composed safely using the `Return` and `Bind` methods

### `Return` ###

* Given an integer, lift it into an instance of `Maybe<int>`

``` csharp
var result = Maybe<int>.Return(42);
// result : Maybe<int> [Some(42)]
```

### `Bind` ###

* Given an instance of `Maybe<int>`, and a function which takes an `int` to `Maybe<bool>`, chain them together so that function is only invoked when an integer value is present

``` csharp
var result = Maybe<42>.Bind(v => Maybe<bool>.Some(v % 2 == 0));
// result : Maybe<bool> [Some(true)]
```

## LINQ extension ##

C# provides a mechanism for obtaining LINQ Syntactic Sugar for monadic composition.

* Given two instances of `Maybe<int>`, return their sum

``` csharp
var result = from a in Maybe<int>.Some(4)
    from b in Maybe<int>.Some(6)
    select a + b;
// result : Maybe<int> [Some(10)]
```

## Changelog

* EDIT: Feb 7 2020 - Incorporated `Combine` from @ericrey85. Thank you!
* EDIT: Feb 7 2020 - Added `GetOrDefault` as suggested to @rulrok.