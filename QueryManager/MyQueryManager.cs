using System.Collections.Generic;
using QUERYMANAGER.DeleteBuilder;
public class MyQueryManager : IQueryManager 
{
    private Connection connection;
    private List<IQuery> queries;
    //private IQueryBuilder builder;
    IQuery Insert(string tableName, IDictionary<string, object> columnValues)
    {
        try
        {
            if(string.IsNullOrEmpty(tableName))
            {
                throw new Exception("Empty table name!");
            }
            if(columnValues.Count < 1)
            {
                throw new Exception("Empty value table!");
            }
            IQueryBuilder builder = new InsertBuilder();
            builder.setStatement(tableName);
            builder.setValue(columnValues);
            _query = builder.result();
            _query.initializeMemento();
            queries.Add(_query);
            return _query;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    // issues a SQL UPDATE operation for tableName setting the provided column
    // values and creating a WHERE clause from the predicates
    //
    // an IList<IPredicate> is an order list of predicate objects.
    //
    // java developers can think of IList much like ArrayList if that helps
    IQuery Update(string tableName, Dictionary<string, object> columnValues, IList<IPredicate> predicates)
    {
        try
        {
            if(string.IsNullOrEmpty(tableName))
            {
                throw new Exception("Empty table name!");
            }
            if(columnValues.Count < 1)
            {
                throw new Exception("Empty value table!");
            }
            IQueryBuilder builder = new UpdateBuilder();
            builder.SetStatement(tableName);
            builder.SetValue(columnValues);
            builder.SetWhere(predicates);
            IQuery _query = builder.result();
            _query.initializeMemento();
            queries.Add(_query);
            return _query;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    // issues a SQL DELETE operation for tableName with a WHERE clause
    // from the predicates
    IQuery Delete(string tableName, IList<IPredicate> predicates)
    {
        try
        {
            if(string.IsNullOrEmpty(tableName))
            {
                throw new Exception("Empty table name!");
            }
            if(columnValues.Count < 1)
            {
                throw new Exception("Empty value table!");
            }
            IQueryBuilder builder = new DeleteBuilder();
            builder.SetStatement(tableName);
            builder.SetWhere(predicates);
            IQuery _query = builder.result();
            _query.initializeMemento();
            queries.Add(_query);
            return _query;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    // causes the requested database operations to execute (returns true if at
    // least one row is effected), check the individual IQuery returns
    // from the above methods to determine the actual impact of each
    bool Commit()
    {
        if(queries.Count == 0)
        {
            return false;
        }
        int count = 0;
        foreach(IQuery query in queries)
        {
            SqlCommand cmd= new SqlCommand(query.toQueryString(), connection);
            cmd.Connection().open();
            query.setState(myCmdObject.ExecuteNonQuery());
            cmd.Connection().close();
            
            if(query.getState() != -1)
            {
                count ++;
            }
        }
        return count > 0; 
    }

    // abandons the requested database operations (returns false if there are
    // no operations)
    bool Rollback()
    {
        if(queries.Count == 0)
        {
            return false;
        }
        queries = new List<IQuery>();
        return true;
    }
}