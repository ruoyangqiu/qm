public interface IPredicate
{
public string ColumnName { get; }
public BinaryOperation Operation { get; }
public object Value { get; }
}