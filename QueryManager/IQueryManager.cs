
 

public interface IQueryManager
{
// issues a SQL INSERT operation for tableName setting the provided
// column values
//
// an IDictionary<string, object> is an interface that maps string keys
// onto object values, for java developers think Map<String, Object> and
// for javascript/typescript developers think 'hash'
IQuery Insert(string tableName,
IDictionary<string, object> columnValues);
// issues a SQL UPDATE operation for tableName setting the provided column
// values and creating a WHERE clause from the predicates
//
// an IList<IPredicate> is an order list of predicate objects.
//
// java developers can think of IList much like ArrayList if that helps
IQuery Update(string tableName,
IDictionary<string, object> columnValues, IList<IPredicate> predicates);
// issues a SQL DELETE operation for tableName with a WHERE clause
// from the predicates
IQuery Delete(string tableName,
IList<IPredicate> predicates);
// causes the requested database operations to execute (returns true if at
// least one row is effected), check the individual IQuery returns
// from the above methods to determine the actual impact of each
bool Commit();
// abandons the requested database operations (returns false if there are
// no operations)
bool Rollback();
}
