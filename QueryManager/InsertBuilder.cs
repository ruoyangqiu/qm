public class InsertBuilder : IQueryBuilder
{
    private IQuery _query = new UpdateQuery();
    public void BuildStatement(string tablename)
    {
        _query.SetStatement(tablename);
    }
        
    public void BuildValue(IDictionary<string, object> columnValues)
    {
        _query.SetValue(columnValues);
    }
        
    public void BuildWhere(IList<IPredicate> predicates)
    {
        _query.SetWhere(predicates);
    }

    public IQuery result()
    {
        return _query;
    }

    public int getState()
    {
        return originator.getState();
    }

    void setState(int state)
    {
        originator.setState(state);
    }

    void initializeMemento()
    {
        originator.setState(0);
        caretaker.setMemento(originator.createMemento());
    }

}