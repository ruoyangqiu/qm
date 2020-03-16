public class UpdateBuilder : IQueryBuilder
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
}