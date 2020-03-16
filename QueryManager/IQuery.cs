public interface IQuery
{
    void SetStatement(string tablename);
        
    void SetValue(IDictionary<string, object> columnValues);
        
    void SetWhere(IList<IPredicate> predicates);

    string toQueryString();

    int getState();

    void setState(int state);

    void initializeMemento();
}