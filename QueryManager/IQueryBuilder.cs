
 public interface IBuilder
{
    void BuildStatement(string tablename);
        
    void BuildValue(IDictionary<string, object> columnValues);
        
    void BuildWhere(IList<IPredicate> predicates);

    IQuery result();
}