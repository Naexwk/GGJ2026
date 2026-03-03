// Predicate interface
// Makes sure all clases that inherit from it have an evaluate function (like FuncPredicate)
public interface IPredicate
{
    bool Evaluate();
}